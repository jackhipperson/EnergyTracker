using EnergyTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.Account
{
    public class LogOutModel(SignInManager<UserModel> signInManager) : PageModel
    {
        private readonly SignInManager<UserModel> signInManager = signInManager;

        public async Task<IActionResult> OnGet()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Account/LogIn");
        }
    }
}
