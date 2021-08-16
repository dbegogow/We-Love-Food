using System.Linq;
using WeLoveFood.Data.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.clients;
using WeLoveFood.Services.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.WebConstants;
using static WeLoveFood.Data.DataConstants.User;
using static WeLoveFood.Areas.Identity.Pages.Account.Constants.ValidationErrorMessages;

namespace WeLoveFood.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private const string IsManagerDisplayName = "Регистрирай ме като ресторантюр";
        private const string DuplicateUserNameErrorCode = "DuplicateUserName";

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IClientsService _clients;
        private readonly IManagersService _managers;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IClientsService clients,
            IManagersService managers)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._clients = clients;
            this._managers = managers;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = RequiredEmail)]
            [EmailAddress(ErrorMessage = InvalidEmail)]
            public string Email { get; set; }

            [Required(ErrorMessage = RequiredPassword)]
            [StringLength(PasswordMaxLength, ErrorMessage = InvalidPasswordLength, MinimumLength = PasswordMinLength)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare(nameof(Password), ErrorMessage = InvalidPasswordConfirmation)]
            public string ConfirmPassword { get; set; }

            [Display(Name = IsManagerDisplayName)]
            public bool IsManager { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                };

                var result = await this._userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    if (Input.IsManager)
                    {
                        this._managers.CreateManager(user.Id);
                        await _userManager.AddToRoleAsync(user, ManagerRoleName);
                    }
                    else
                    {
                        this._clients.CreateClient(user.Id);
                        await _userManager.AddToRoleAsync(user, ClientRoleName);
                    }

                    await this._signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                var hasDuplicateUserNameInvalid = result
                    .Errors
                    .Any(e => e.Code == DuplicateUserNameErrorCode);

                ModelState.AddModelError("#",
                    hasDuplicateUserNameInvalid ? AlreadyExistUserWithEmail : InvalidPasswordContent);
            }

            return Page();
        }
    }
}
