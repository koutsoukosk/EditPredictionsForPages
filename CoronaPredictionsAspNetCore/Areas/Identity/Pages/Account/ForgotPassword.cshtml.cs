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
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }
         [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]          
            public string Email { get; set; }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword( )
        {
            return Page();
        }

        [HttpPost]
        [AllowAnonymous]
       // [FromBody]
        //ForgotPasswordModel model
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user!=null && await _userManager.IsEmailConfirmedAsync(user)) {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordResetLink= Url.Page( "/Account/ResetPassword",
                                                    pageHandler: null,
                                                    values: new { email = Input.Email, token = token },
                                                    protocol: Request.Scheme);
                    _logger.Log(LogLevel.Warning, passwordResetLink);
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
                return RedirectToPage("./ForgotPasswordConfirmation");
            }
             return Page();
        }


    }
}
