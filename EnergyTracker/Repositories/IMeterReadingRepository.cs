using EnergyTracker.Models;

namespace EnergyTracker.Repositories
{
    public interface IMeterReadingRepository
    {

        public Task<MeterReadingModel> AddMeterReading(MeterReadingModel meterReading);

        public Task<MeterReadingModel> EditMeterReading(MeterReadingModel meterReading);

        public Task<bool> DeleteReading(Guid id);

        public Task<MeterReadingModel> GetMeterReading(Guid id);
        public Task<IEnumerable<MeterReadingModel>> GetAllReadingsAsync(Guid userId);
    }
}
