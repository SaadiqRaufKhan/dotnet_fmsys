using Microsoft.EntityFrameworkCore;
using fleetsystem.entity;

namespace fleetsystem.config;

public class TruckDbContext : DbContext
{
    public TruckDbContext(DbContextOptions<TruckDbContext> options) : base(options) { }

    public DbSet<Truck> Trucks { get; set; }
    public DbSet<Driver> Drivers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Truck>().HasKey(t => t.Id);
        modelBuilder.Entity<Driver>().HasKey(d => d.Id);
        base.OnModelCreating(modelBuilder);
    }
}
