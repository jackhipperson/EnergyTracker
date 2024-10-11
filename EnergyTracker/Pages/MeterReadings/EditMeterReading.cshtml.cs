using EnergyTracker.Models;
using EnergyTracker.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnergyTracker.Pages.MeterReadings
{
    public class EditMeterReadingModel : PageModel
    {
        private readonly IMeterReadingRepository meterReadingRepository;

        public EditMeterReadingModel(IMeterReadingRepository meterReadingRepository)
        {
            this.meterReadingRepository = meterReadingRepository;
        }

        public MeterReadingModel MeterReading { get; set; }
        public async Task OnGet(Guid id)
        {
            var meterReading = await meterReadingRepository.GetMeterReading(id);

            if (meterReading != null)
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
}
