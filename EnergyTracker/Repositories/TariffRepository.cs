using EnergyTracker.Data;
using EnergyTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyTracker.Repositories
{
    public class TariffRepository : ITariffRepository
    {
        private readonly AppDbContext dbContext;

        public TariffRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<TariffModel> AddTariff(TariffModel tariff)
        {
            await dbContext.Tariffs.AddAsync(tariff);
            await dbContext.SaveChangesAsync();
            return tariff;
        }

        public async Task<bool> DeleteTariff(Guid id)
        {
            TariffModel tariffToDelete = await dbContext.Tariffs.FirstOrDefaultAsync(t => t.Id == id);
            if (tariffToDelete != null)
            {
                dbContext.Tariffs.Remove(tariffToDelete);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<TariffModel> EditTariff(TariffModel tariff)
        {
            TariffModel editingTariff = await dbContext.Tariffs.FirstOrDefaultAsync(x => x.Id == tariff.Id);
            if (editingTariff != null)
            {
                editingTariff.StartDate = tariff.StartDate;
                editingTariff.EndDate = tariff.EndDate;
                editingTariff.ElectricUnitRate = tariff.ElectricUnitRate;
                editingTariff.ElectricStandingRate = tariff.ElectricStandingRate;
                editingTariff.GasUnitRate = tariff.GasUnitRate;
                editingTariff.GasStandingRate = tariff.GasStandingRate;
            }
            await dbContext.SaveChangesAsync();
            return tariff;
        }

        public async Task<IEnumerable<TariffModel>> GetAllTariffsAsync(Guid userId)
        {
            return await dbContext.Tariffs.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<TariffModel> GetTariffAsync(Guid id)
        {
            return await dbContext.Tariffs.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

    }
}
