using System.Linq;
using System.Threading.Tasks;
using WeLoveFood.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.User;
using static WeLoveFood.Areas.Identity.Pages.Account.Constants.ValidationErrorMessages;

namespace WeLoveFood.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private const string CurrentPasswordDisplayName = "Сегашна парола";
        private const string NewPasswordDisplayName = "Нова парола";
        private const string ConfirmNewPasswordDisplayName = "Повтори парола";

        private const string PasswordMismatchModelErrorCode = "PasswordMismatch";

        private const string LogInInformationText = "User changed their password successfully.";
        private const string StatusMessageText = "Твоята парола бе успешно сменена.";

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = RequiredField)]
            [DataType(DataType.Password)]
            [Display(Name = CurrentPasswordDisplayName)]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = RequiredField)]
            [StringLength(PasswordMaxLength, ErrorMessage = InvalidPasswordLength, MinimumLength = PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = NewPasswordDisplayName)]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = ConfirmNewPasswordDisplayName)]
            [Compare(nameof(NewPassword), ErrorMessage = InvalidPasswordConfirmation)]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this._userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            var hasPassword = await this._userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await this._userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            var changePasswordResult = await this._userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                var isCurrentPasswordInvalid = changePasswordResult
                    .Errors
                    .Any(e => e.Code == PasswordMismatchModelErrorCode);

                ModelState.AddModelError("#",
                    isCurrentPasswordInvalid ? InvalidPassword : InvalidPasswordContent);

                return Page();
            }

            await this._signInManager.RefreshSignInAsync(user);
            this._logger.LogInformation(LogInInformationText);
            this.StatusMessage = StatusMessageText;

            return RedirectToPage();
        }
    }
}
