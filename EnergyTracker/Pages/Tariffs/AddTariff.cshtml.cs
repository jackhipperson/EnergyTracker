using EnergyTracker.Models;
using EnergyTracker.Models.ViewModels;
using EnergyTracker.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.Tariffs
{
    public class AddTariffModel : PageModel
    {
        private readonly ITariffRepository tariffRepository;

        public AddTariffModel(ITariffRepository tariffRepository)
        {
            this.tariffRepository = tariffRepository;
        }

        [BindProperty]
        public Tariff Tariff { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
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
                    newTariff.UserId = Tariff.UserId;
                }
                await tariffRepository.AddTariff(newTariff);
                return RedirectToPage("Tariffs/ViewTariffs");
            }
            return Page();
        }
    }
}
