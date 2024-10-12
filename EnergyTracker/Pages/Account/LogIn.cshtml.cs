using EnergyTracker.Models;
using EnergyTracker.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.Account
{
    public class LogInModel : PageModel
    {
        private readonly SignInManager<UserModel> signInManager;

        public LogInModel(SignInManager<UserModel> signInManager)
        {
            this.signInManager = signInManager;
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

                var result = await signInManager.PasswordSignInAsync(User.UserName, User.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Unable to log in, please check your username and password and try again.");
                }
            }
            return Page();

        }
    }

}
