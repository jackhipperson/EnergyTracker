using EnergyTracker.Models;
using EnergyTracker.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.Tariffs
{
    public class EditTariffModel(ITariffRepository tariffRepository, UserManager<UserModel> userManager) : PageModel
    {
        private readonly ITariffRepository tariffRepository = tariffRepository;
        private readonly UserManager<UserModel> userManager = userManager;

        public TariffModel Tariff { get; set; }
        public async Task OnGet(Guid tariffId)
        {
            Guid userId = Guid.Parse(userManager.GetUserId(User));
            var tariff = await tariffRepository.GetTariffAsync(tariffId);
            if (Tariff.UserId != userId)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                Tariff = new TariffModel();
                {
                    Tariff.Id = tariff.Id;
                    Tariff.UserId = tariff.UserId;
                    Tariff.ElectricUnitRate = tariff.ElectricUnitRate;
                    Tariff.ElectricStandingRate = tariff.ElectricStandingRate;
                    Tariff.GasUnitRate = tariff.GasUnitRate;
                    Tariff.GasStandingRate = tariff.GasStandingRate;
                    Tariff.StartDate = tariff.StartDate;
                    Tariff.EndDate = tariff.EndDate;
                }
            }

        }
    }
}
