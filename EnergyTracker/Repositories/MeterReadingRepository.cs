using EnergyTracker.Data;
using EnergyTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyTracker.Repositories
{
    public class MeterReadingRepository(AppDbContext dbContext) : IMeterReadingRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<MeterReadingModel> AddMeterReading(MeterReadingModel meterReading)
        {
            await dbContext.MeterReadings.AddAsync(meterReading);
            dbContext.SaveChanges();
            return meterReading;
        }

        public async Task<bool> DeleteReading(Guid id)
        {
            MeterReadingModel reading = await dbContext.MeterReadings.FindAsync(id);
            if (reading != null)
            {
                dbContext.MeterReadings.Remove(reading);
                await dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<MeterReadingModel> EditMeterReading(MeterReadingModel meterReading)
        {
            var editingReading = await dbContext.MeterReadings.FindAsync(meterReading.Id);
            if (editingReading != null)
            {
                editingReading.ElectricReading = meterReading.ElectricReading;
                editingReading.GasReading = meterReading.GasReading;
                editingReading.ReadingDate = meterReading.ReadingDate;
            }
            dbContext.SaveChanges();
            return meterReading;
        }

        public async Task<IEnumerable<MeterReadingModel>> GetAllReadingsAsync(Guid userId)
        {
            return await dbContext.MeterReadings.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<MeterReadingModel> GetMeterReading(Guid id)
        {
            return await dbContext.MeterReadings.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
