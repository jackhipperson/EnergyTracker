using EnergyTracker.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.LogIn
{
    public class LogInModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public LogInModel(SignInManager<IdentityUser> signInManager)
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
                var user = new IdentityUser();
                {
                    user.UserName = User.UserName;
                }

                var result = await signInManager.PasswordSignInAsync(user, User.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToPage("/");
                }
            }
            return Page();

        }
    }

}
