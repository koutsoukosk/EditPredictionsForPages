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
    [Authorize(Roles ="Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListUsers");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var user = await _roleManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListRoles");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName,
                Claims=userClaims.Select(x=>x.Value).ToList(),
                Roles=userRoles
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserModel modelUpdate)
        {
            var user = await _userManager.FindByIdAsync(modelUpdate.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {modelUpdate.Id} cannot be found";
                return View("NotFound");
            } else{
                user.Email = modelUpdate.Email;
                user.UserName = modelUpdate.UserName;
                user.FullName = modelUpdate.FullName;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(modelUpdate);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            List<ApplicationUser> notRoledUser = new List<ApplicationUser>();
            List<ApplicationUser> allUsers = new List<ApplicationUser>();
            var users = _userManager.Users;
            foreach (var item in users)
            {
                if ((await _userManager.IsInRoleAsync(item, "Admin"))|| (await _userManager.IsInRoleAsync(item, "User")))
                {
                    allUsers.Add(item);
                }
                else
                {
                    notRoledUser.Add(item);
                }
            }
            allUsers.AddRange(notRoledUser);
            return View(allUsers);
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