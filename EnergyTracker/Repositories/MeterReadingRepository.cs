using EnergyTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyTracker.Repositories
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly AppDbContext dbContext;

        public MeterReadingRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
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

        public Task<MeterReadingModel> EditMeterReading(MeterReadingModel meterReading)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MeterReadingModel>> GetAllReadingsAsync()
        {
            return await dbContext.MeterReadings.ToListAsync();
        }

        public async Task<MeterReadingModel> GetMeterReading(Guid id)
        {
            return await dbContext.MeterReadings.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
