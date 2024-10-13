using EnergyTracker.Models;
using EnergyTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Authentication;

namespace EnergyTracker.Pages.MeterReadings
{
    [Authorize]
    public class EditMeterReadingModel(IMeterReadingRepository meterReadingRepository, UserManager<UserModel> userManager) : PageModel
    {
        private readonly IMeterReadingRepository meterReadingRepository = meterReadingRepository;
        private readonly UserManager<UserModel> userManager = userManager;

        [BindProperty]
        public MeterReadingModel MeterReading { get; set; }
        public async Task OnGet(Guid id)
        {
            Guid userId = Guid.Parse(userManager.GetUserId(User));
            var meterReading = await meterReadingRepository.GetMeterReading(id);

            if (meterReading != null)
            {
                if (meterReading.UserId != userId)
                {
                    throw new AuthenticationException();
                }
                else
                {
                    MeterReading = new MeterReadingModel
                    {
                        Id = meterReading.Id,
                        ElectricReading = meterReading.ElectricReading,
                        GasReading = meterReading.GasReading,
                        ReadingDate = meterReading.ReadingDate,
                        UserId = meterReading.UserId
                    };
                }
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Guid userId = Guid.Parse(userManager.GetUserId(User));
                if (MeterReading.UserId == userId)
                {

                    await meterReadingRepository.EditMeterReading(MeterReading);

                    return RedirectToPage("/MeterReadings/ViewMeterReadings");

                }
            }

            return Page();
        }
    }
}
