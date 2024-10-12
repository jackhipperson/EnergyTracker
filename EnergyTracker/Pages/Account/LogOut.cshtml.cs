using EnergyTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.Account
{
    public class LogOutModel : PageModel
    {
        private readonly SignInManager<UserModel> signInManager;

        public LogOutModel(SignInManager<UserModel> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Account/LogIn");
        }
    }
}
