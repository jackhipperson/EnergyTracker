using EnergyTracker.Models;
using EnergyTracker.Models.ViewModels;
using EnergyTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.MeterReadings
{
    [Authorize]
    public class AddMeterReadingModel(IMeterReadingRepository meterReadingRepository, UserManager<UserModel> userManager) : PageModel
    {
        private readonly IMeterReadingRepository meterReadingRepository = meterReadingRepository;
        private readonly UserManager<UserModel> userManager = userManager;

        [BindProperty]
        public MeterReading AddedMeterReading { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                string userId = userManager.GetUserId(User);
                MeterReadingModel submittedMeterReading = new()
                {
                    Id = new Guid(),
                    ElectricReading = AddedMeterReading.ElectricReading,
                    GasReading = AddedMeterReading.GasReading,
                    ReadingDate = AddedMeterReading.ReadingDate,
                    UserId = Guid.Parse(userId),
                };

                await meterReadingRepository.AddMeterReading(submittedMeterReading);

                return RedirectToPage("/MeterReadings/ViewMeterReadings");
            }

            return Page();

        }
    }
}
