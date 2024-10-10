using EnergyTracker.Models;
using EnergyTracker.Models.ViewModels;
using EnergyTracker.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages
{
    public class AddMeterReadingModel : PageModel
    {
        private readonly IMeterReadingRepository meterReadingRepository;

        public AddMeterReadingModel(IMeterReadingRepository meterReadingRepository)
        {
            this.meterReadingRepository = meterReadingRepository;
        }

        [BindProperty]
        public MeterReading AddedMeterReading { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                MeterReadingModel submittedMeterReading = new MeterReadingModel()
                {
                    Id = new Guid(),
                    ElectricReading = AddedMeterReading.ElectricReading,
                    GasReading = AddedMeterReading.GasReading,
                    ReadingDate = AddedMeterReading.ReadingDate,
                    UserId = AddedMeterReading.UserId,
                };

                await meterReadingRepository.AddMeterReading(submittedMeterReading);

                return RedirectToPage("/Index");
            }

            return Page();

        }
    }
}
