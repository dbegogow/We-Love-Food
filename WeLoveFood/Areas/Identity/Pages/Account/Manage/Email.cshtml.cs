using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WeLoveFood.Web.Data;
using WeLoveFood.Web.Data.Models;
using static WeLoveFood.Web.Data.DataConstants.User;
using static WeLoveFood.Web.Areas.Identity.Pages.Account.Constants.ValidationErrorMessages;

namespace WeLoveFood.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class EmailModel : PageModel
    {
        private const string NewEmailDisplayName = "Нов имейл";
        private const string PasswordDisplayName = "Парола";

        private const string SuccessMessageText = "Успешно сменихте своя имейл.";

        private readonly UserManager<User> _userManager;
        private readonly WeLoveFoodDbContext _data;

        public EmailModel(
            UserManager<User> userManager,
            WeLoveFoodDbContext data)
        {
            _userManager = userManager;
            _data = data;
        }

        public string Email { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = RequiredEmail)]
            [EmailAddress]
            [Display(Name = NewEmailDisplayName)]
            public string NewEmail { get; set; }

            [Required(ErrorMessage = RequiredField)]
            [StringLength(PasswordMaxLength, ErrorMessage = InvalidPasswordLength, MinimumLength = PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = PasswordDisplayName)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this._userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            this.Email = await this._userManager.GetEmailAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newEmail = Input.NewEmail;

            var isNewEmailAlreadyExist = this._data
                .Users
                .Any(u => u.Email == newEmail);

            if (isNewEmailAlreadyExist)
            {
                ModelState.AddModelError("#", AlreadyExistUserWithEmail);
                return Page();
            }

            var verifyHashedPasswordResult = this._userManager
                .PasswordHasher
                .VerifyHashedPassword(user, user.PasswordHash, Input.Password);

            if (verifyHashedPasswordResult != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("#", InvalidPassword);
                return Page();
            }

            var token = await this._userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            var changeEmailResult = await this._userManager.ChangeEmailAsync(user, newEmail, token);
            var changeUserNameResult = await this._userManager.SetUserNameAsync(user, newEmail);

            if (!changeEmailResult.Succeeded ||
                !changeUserNameResult.Succeeded)
            {
                ModelState.AddModelError("#", InvalidChangeEmail);
                return Page();
            }

            StatusMessage = SuccessMessageText;
            return RedirectToPage();
        }
    }
}
