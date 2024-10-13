using EnergyTracker.Models;
using EnergyTracker.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.Account
{
    public class RegisterModel(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager) : PageModel
    {
        private readonly UserManager<UserModel> userManager = userManager;
        private readonly SignInManager<UserModel> signInManager = signInManager;

        [BindProperty]
        public UserViewModel User { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                UserModel user = new();
                {
                    user.UserName = User.UserName;
                }
                var identityResult = await userManager.CreateAsync(user, User.Password);
                if (identityResult.Succeeded)
                {
                    await signInManager.PasswordSignInAsync(user, User.Password, false, false);
                    return RedirectToPage("/");
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
