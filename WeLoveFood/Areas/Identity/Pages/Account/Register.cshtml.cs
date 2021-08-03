using System.Linq;
using WeLoveFood.Data.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.clients;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.User;
using static WeLoveFood.Areas.Identity.Pages.Account.Constants.ValidationErrorMessages;

namespace WeLoveFood.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private const string DuplicateUserNameErrorCode = "DuplicateUserName";

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IClientsService _clients;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IClientsService clients)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._clients = clients;
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
                    this._clients.CreateClient(user.Id);

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
