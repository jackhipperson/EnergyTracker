using EnergyTracker.Models;
using EnergyTracker.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.MeterReadings
{
    public class ViewMeterReadingsModel : PageModel
    {
        private readonly IMeterReadingRepository meterReadingRepository;

        public ViewMeterReadingsModel(IMeterReadingRepository meterReadingRepository)
        {
            this.meterReadingRepository = meterReadingRepository;
        }

        public List<MeterReadingModel> MeterReadings { get; set; }

        public void OnGet()
        {
            MeterReadings = meterReadingRepository.GetAllReadingsAsync().Result.ToList();
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            await meterReadingRepository.DeleteReading(id);
            return RedirectToPage("/ViewMeterReadings");
        }
    }
}
