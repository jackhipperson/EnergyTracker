using EnergyTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnergyTracker.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserModel>(options)
    {
        public DbSet<MeterReadingModel> MeterReadings { get; set; }
        public DbSet<TariffModel> Tariffs { get; set; }
    }
}


