using EnergyTracker.Models;
using EnergyTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.MeterReadings
{
    [Authorize]
    public class ViewMeterReadingsModel(IMeterReadingRepository meterReadingRepository, UserManager<UserModel> userManager) : PageModel
    {
        private readonly IMeterReadingRepository meterReadingRepository = meterReadingRepository;
        private readonly UserManager<UserModel> userManager = userManager;

        public List<MeterReadingModel> MeterReadings { get; set; }

        public void OnGet()
        {
            Guid userId = Guid.Parse(userManager.GetUserId(User));

            MeterReadings = meterReadingRepository.GetAllReadingsAsync(userId).Result.ToList();
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            await meterReadingRepository.DeleteReading(id);
            return RedirectToPage("/MeterReadings/ViewMeterReadings");
        }
    }
}
