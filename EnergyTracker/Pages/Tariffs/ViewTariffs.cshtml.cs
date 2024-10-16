using EnergyTracker.Models;
using EnergyTracker.Models.ViewModels;
using EnergyTracker.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EnergyTracker.Pages.Tariffs
{
    public class ViewTariffsModel(ITariffRepository tariffRepository, UserManager<UserModel> userManager) : PageModel
    {
        private readonly ITariffRepository tariffRepository = tariffRepository;
        private readonly UserManager<UserModel> userManager = userManager;

        public IEnumerable<TariffModel> UserTariffs { get; set; }
        public List<TariffModel> GapList { get; set; }
        public List<TariffModel> OverlapList { get; set; }
        public async void OnGet()
        {
            Guid userId = Guid.Parse(userManager.GetUserId(User));
            UserTariffs = await tariffRepository.GetAllTariffsAsync(userId);

            GapList = await tariffRepository.GetGapsAsync(userId);
            OverlapList = await tariffRepository.GetOverlapsAsync(userId);

        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            await tariffRepository.DeleteTariff(id);
            return RedirectToPage("/Tariffs/ViewTariffs");
        }
    }
}
