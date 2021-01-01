using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CoronaPredictionsAspNetCore.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace CoronaPredictionsAspNetCore.Areas.Identity.Pages.Account
{
    //[AllowAnonymous]
    //public class ConfirmEmailModel : PageModel
    //{
    //    private readonly SignInManager<ApplicationUser> _signInManager;
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly ILogger<RegisterModel> _logger;
    //    private readonly IEmailSender _emailSender;

        //public ConfirmEmailModel(
        //    UserManager<ApplicationUser> userManager,
        //    SignInManager<ApplicationUser> signInManager,
        //    ILogger<RegisterModel> logger,
        //    IEmailSender emailSender)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _logger = logger;
        //    _emailSender = emailSender;
        //}
        // [BindProperty]
        //public InputModel Input { get; set; }

        //public string ReturnUrl { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }

        //public class InputModel
        //{
        //    [Required]
        //    [DataType(DataType.Text)]
        //    [Display(Name = "Full Name")]
        //    public string FullName { get; set; }

        //    [Required]
        //    [EmailAddress]
        //    [Display(Name = "Email")]
        //    [PageRemote(PageHandler = "IsEmailInUse", HttpMethod ="get", ErrorMessage = "Email is already in use")]
        //    public string Email { get; set; }

        //    [Required]
        //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        //    [DataType(DataType.Password)]
        //    [Display(Name = "Password")]
        //    public string Password { get; set; }

        //    [DataType(DataType.Password)]
        //    [Display(Name = "Confirm password")]
        //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //    public string ConfirmPassword { get; set; }
        //}

        //public async Task OnGetAsync(string returnUrl = null)
        //{           
        //    //if (User.Identity.IsAuthenticated)
        //    //{
        //    //    Response.Redirect("/");
        //    //}          
        //    ReturnUrl = returnUrl;
        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        //}

        //[AcceptVerbs("Get", "Post")]
        //[AllowAnonymous]
        //public JsonResult OnGetIsEmailInUse(InputModel Input)
        //{
        //    var user =  _userManager.FindByEmailAsync(Input.Email);
        //    if (user.Result == null)
        //    {
        //        return new JsonResult(true);
        //    }
        //    else
        //    {
        //        return new JsonResult($"Email { Input.Email} is already in use");
        //    }
        //}
        
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ConfirmEmail(string userId, string code)
        //{
        //    if (userId==null || code == null)
        //    {
        //        return RedirectToAction("index", "home");
        //    }
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user==null)
        //    {
        //        ModelState.AddModelError("Registration successful", $"The User ID {userId} is invalid");
        //        return Page();
        //    }
        //    var result = await _userManager.ConfirmEmailAsync(user, code);
        //    if (result.Succeeded)
        //    {
        //        return Page();
        //    }
        //    ModelState.AddModelError(string.Empty, "Email cannot be confirmed");
        //    return Page();
        //}


   // }
}
