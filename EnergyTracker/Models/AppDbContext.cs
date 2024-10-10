﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnergyTracker.Models
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MeterReadingModel> MeterReadings { get; set; }
        public DbSet<TariffModel> Tariffs { get; set; }
    }
}
