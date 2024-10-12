using EnergyTracker.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.LogIn
{
    public class SignUpModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public SignUpModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public UserViewModel User { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new();
                {
                    user.UserName = User.UserName;
                }
                var identityResult = await userManager.CreateAsync(user, User.Password);
                if (identityResult.Succeeded)
                {
                    return RedirectToPage("/LogIn/LogIn");
                }
                else
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }
            }
            return Page();
        }
    }
}
