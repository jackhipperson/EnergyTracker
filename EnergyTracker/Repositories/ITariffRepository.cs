using EnergyTracker.Models;

namespace EnergyTracker.Repositories
{
    public interface ITariffRepository
    {
        public Task<TariffModel> GetTariffAsync(Guid id);
        public Task<IEnumerable<TariffModel>> GetAllTariffsAsync(Guid userId);
        public Task<TariffModel> AddTariff(TariffModel tariff);
        public Task<TariffModel> EditTariff(TariffModel tariff);
        public Task<bool> DeleteTariff(Guid id);

    }
}
