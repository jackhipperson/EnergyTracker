using EnergyTracker.Models;
using EnergyTracker.Models.ViewModels;
using EnergyTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.Tariffs
{
    [Authorize]
    public class AddTariffModel(UserManager<UserModel> userManager, ITariffRepository tariffRepository) : PageModel
    {
        private readonly UserManager<UserModel> userManager = userManager;
        private readonly ITariffRepository tariffRepository = tariffRepository;

        [BindProperty]
        public Tariff Tariff { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                string userId = userManager.GetUserId(User);
                TariffModel newTariff = new();
                {
                    newTariff.Id = new Guid();
                    newTariff.Description = Tariff.Description;
                    newTariff.ElectricStandingRate = Tariff.ElectricStandingRate;
                    newTariff.ElectricUnitRate = Tariff.ElectricUnitRate;
                    newTariff.GasStandingRate = Tariff.GasStandingRate;
                    newTariff.GasUnitRate = Tariff.GasUnitRate;
                    newTariff.StartDate = Tariff.StartDate;
                    newTariff.EndDate = Tariff.EndDate;
                    newTariff.UserId = Guid.Parse(userId);
                }
                await tariffRepository.AddTariff(newTariff);
                return RedirectToPage("Tariffs/ViewTariffs");
            }
            return Page();
        }
    }
}
