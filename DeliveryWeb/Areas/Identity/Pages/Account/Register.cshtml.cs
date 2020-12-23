using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using DeliveryWeb.Models;
using DeliveryWeb.Data;
using Microsoft.EntityFrameworkCore;
using DeliveryWeb.Utility;
using NETCore.MailKit.Core;

namespace DeliveryWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext Db;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailService emailService,
            ApplicationDbContext Db,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _userManager.Options.Password.RequireLowercase = false;
            _userManager.Options.Password.RequireUppercase = false;
            _userManager.Options.Password.RequireNonAlphanumeric = false;

            _signInManager = signInManager;
            _logger = logger;
            _emailService = emailService;
            this.Db = Db;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
            [EmailAddress]
            [Display(Name = "الايميل")]
            public string Email { get; set; }

            [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
            [StringLength(100, ErrorMessage = "يجب أن تحتوي كلمة المرور {2} رموز على الاقل ", MinimumLength = 6)]
            [RegularExpression(@"^((?=.*[aA-zZ])(?=.*\d)).+$", ErrorMessage ="يجب ان تحتوي كلمة المرور على حرف انجليزي و رقم ")]
            [DataType(DataType.Password)]
            [Display(Name = "كلمة المرور")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تأكيد كلمة المرور")]
            [Compare("Password", ErrorMessage = "كلمة المرور غير متطابقة")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "كود الحساب")]
            public string Code { get; set; }

            [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
            [Display(Name = "اسم المتجر")]
            public string Name { get; set; }

            [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
            [Display(Name = "رقم الهاتف 1")]
            public string Phone1 { get; set; }

            [Required]
            [Display(Name = "رقم الهاتف 2")]
            public string Phone2 { get; set; }

            [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
            [Display(Name = "العنوان")]
            public string Address { get; set; }

            public bool Exist { get; set; }

            public bool? EmailError = false;

            public string Role { get; set; }


        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var cd = await Db.Customers.MaxAsync(item => item.Code);
            var newcode = (int.Parse(cd) + 1).ToString("D5");

            string role = HttpContext.Request.Form["rdRole"].ToString();
            if (ModelState.IsValid)
            {
                var user = new Models.Customer { 
                    UserName = Input.Email, 
                    Email = Input.Email,
                    Code = newcode,
                    Name = Input.Name,
                    Phone1 = Input.Phone1,
                    Phone2 = Input.Phone2,
                    Address = Input.Address,
                    Role=role
                };

                if (string.IsNullOrEmpty(role)) user.Role = SD.User;

                var result = await _userManager.CreateAsync(user,Input.Password);

                if (result.Succeeded)
                {
                    if(! await _roleManager.RoleExistsAsync(SD.Manager))
                    {
                       await _roleManager.CreateAsync(new IdentityRole(SD.Manager));
                    }

                    if (!await _roleManager.RoleExistsAsync(SD.Delegate))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Delegate));
                    }

                    if (!await _roleManager.RoleExistsAsync(SD.User))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.User));
                    }

                    if (string.IsNullOrEmpty(role))
                    {
                        await _userManager.AddToRoleAsync(user, SD.User);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    
                    await _emailService.SendAsync(Input.Email, "تأكيد حسابك",
                        $"<div>اضغط على الرابط لتأكيد حسابك</div><br/> <a class='btn btn-primary' href='{HtmlEncoder.Default.Encode(callbackUrl)}'>اضغط هنا</a>.",true);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    Input.EmailError = true;
                }
            }

            // If we got this far, something failed, redisplay form
            
            return Page();
        }
    }
}
