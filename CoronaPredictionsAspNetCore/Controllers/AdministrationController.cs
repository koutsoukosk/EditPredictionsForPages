using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaPredictionsAspNetCore.Areas.Identity.Data;
using CoronaPredictionsAspNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoronaPredictionsAspNetCore.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            if (ModelState.IsValid) {
                IdentityRole identityRole = new IdentityRole { Name = model.RoleName };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded) {
                    return RedirectToAction("ListRoles","Administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            if (roleId==null) { return RedirectToAction("EditRole", new { Id = roleId }); }
            var role = await _roleManager.FindByIdAsync(roleId);
            if ( role == null) {
                ViewBag.ErrorMessage = $"Role with Id= {roleId} cannot be found";
                return View();
            }
            var model = new List<UserRoleModel>();
            foreach (var user in _userManager.Users)
            {
                var userRoleModel = new UserRoleModel {UserId=user.Id,UserName=user.UserName };
                if (await _userManager.IsInRoleAsync(user, role.Name)) {
                    userRoleModel.IsSelected = true;
                }
                else
                {
                    userRoleModel.IsSelected = false;
                }
                model.Add(userRoleModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role==null) {
                ViewBag.ErrorMessage = $"Role with Id= {roleId} cannot be found";
                return View("NotFound");
            }
            for (int i=0;i<model.Count;i++) {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name))) {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }else if (!(model[i].IsSelected) && await _userManager.IsInRoleAsync(user, role.Name)) {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                } else {
                    continue;
                }
                if (result.Succeeded) {
                    if (i < model.Count - 1)
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
                
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role=await _roleManager.FindByIdAsync(id);
            if (role==null) {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            var model = new EditRoleModel { Id = role.Id, RoleName = role.Name };
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleModel EdRoModel)
        {
            var role = await _roleManager.FindByIdAsync(EdRoModel.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {EdRoModel.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = EdRoModel.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
                return View(EdRoModel);
            }          
        }
    }
}