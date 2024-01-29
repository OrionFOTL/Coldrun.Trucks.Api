using Coldrun.Trucks.Application.Abstractions.DatabaseContext;
using Coldrun.Trucks.Application.UnitOfWork;
using Coldrun.Trucks.Domain.Entities.Primitives;
using Coldrun.Trucks.Domain.Entities.Trucks;
using Microsoft.EntityFrameworkCore;

namespace Coldrun.Trucks.Persistence;

internal class DatabaseContext(DbContextOptions options) : DbContext(options), IDatabaseContext, IUnitOfWork
{
    public DbSet<Truck> Trucks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Truck>()
            .Property(t => t.Code).HasConversion(
                code => code.Value,
                value => new AlphanumericString(value));
    }
}
