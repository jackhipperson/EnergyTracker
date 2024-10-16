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

        public async Task<List<TariffModel>> GetGapsAsync(Guid userId)
        {
            var gapQuery = @"
        WITH OrderedTariffs AS (
            SELECT 
                Id, 
                Description, 
                StartDate, 
                COALESCE(EndDate, '9999-12-31') AS EndDate,
                UserId,
                ROW_NUMBER() OVER (ORDER BY StartDate) AS RowNum
            FROM 
                Tariffs
            WHERE 
                UserId = {0}
        )
        SELECT 
            CurrentTariff.Id,
            'Gap' AS Description,
            CurrentTariff.EndDate AS StartDate,
            NextTariff.StartDate AS EndDate,
            NULL AS GasUnitRate,
            NULL AS GasStandingRate,
            NULL AS ElectricUnitRate,
            NULL AS ElectricStandingRate,
            CurrentTariff.UserId
        FROM 
            OrderedTariffs CurrentTariff
        INNER JOIN 
            OrderedTariffs NextTariff ON CurrentTariff.RowNum = NextTariff.RowNum - 1
        WHERE 
            CurrentTariff.EndDate < NextTariff.StartDate
        ORDER BY 
            CurrentTariff.EndDate;";

            return await dbContext.Tariffs
                .FromSqlRaw(gapQuery, userId)
                .ToListAsync();
        }


        public async Task<List<TariffModel>> GetOverlapsAsync(Guid userId)
        {
            var overlapQuery = @"
            WITH OrderedTariffs AS (
                SELECT 
                    Id, 
                    Description, 
                    StartDate, 
                    EndDate,
                    UserId, 
                    ROW_NUMBER() OVER (ORDER BY StartDate) AS RowNum
                FROM 
                    Tariffs
                WHERE 
                    UserId = {0}
            )
            SELECT 
                CurrentTariff.Id,
                'Gap' AS Description,
                CurrentTariff.EndDate AS StartDate,
                NextTariff.StartDate AS EndDate,
                NULL AS GasUnitRate,
                NULL AS GasStandingRate,
                NULL AS ElectricUnitRate,
                NULL AS ElectricStandingRate,
                CurrentTariff.UserId
            FROM 
                OrderedTariffs CurrentTariff
            LEFT JOIN 
                OrderedTariffs NextTariff ON CurrentTariff.RowNum = NextTariff.RowNum - 1
            WHERE 
                CurrentTariff.EndDate < NextTariff.StartDate
            ORDER BY 
                CurrentTariff.EndDate;";

            return await dbContext.Set<TariffModel>().FromSqlRaw(overlapQuery, userId).ToListAsync();
        }
    }


}
