using EnergyTracker.Data;
using EnergyTracker.Models;
using EnergyTracker.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnergyTracker.Repositories
{
    public class TariffRepository(AppDbContext dbContext) : ITariffRepository
    {
        private readonly AppDbContext dbContext = dbContext;

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
            return await dbContext.Tariffs.Where(x => x.UserId == userId).OrderBy(x => x.StartDate).ToListAsync();
        }

        public async Task<TariffModel> GetTariffAsync(Guid id)
        {
            return await dbContext.Tariffs.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TariffError>> GetGapsAsync(Guid userId)
        {
            var gapQuery = @"
            SELECT
                t1.Id,
                t1.Description,
                t2.StartDate,
                t1.EndDate,
            FROM
                Tariffs t1,
                Tariffs t2
            WHERE
                t1.UserId = {0} AND
                t2.UserId = {0} AND
                t1.EndDate < t2.StartDate
            ORDER BY
                t1.EndDate;";

            return await dbContext.Tariffs.FromSqlRaw(gapQuery, userId).ToListAsync();
        }

        public async Task<List<TariffError>> GetOverlapsAsync(Guid userId)
        {
            var overlapQuery = @"
            SELECT
                t1.Id
                t1.Description,
                t1.StartDate,
                t1.EndDate,
                t2.Description,
                t2.StartDate,
                t2.EndDate
            FROM
                Tariffs t1,
                Tariffs t2
            WHERE
                t1.UserId = {0} AND
                t2.UserId = {0} AND
                t1.ID != t2.ID AND
                t1.StartDate < t2.EndDate AND
                t2.StartDate < t1.EndDate
            ORDER BY
                t1.StartDate;";

            return await dbContext.Tariffs.FromSqlRaw(overlapQuery, userId).ToListAsync();
        }
    }


}
