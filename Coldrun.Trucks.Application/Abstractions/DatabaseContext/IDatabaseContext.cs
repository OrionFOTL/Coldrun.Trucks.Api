using Coldrun.Trucks.Domain.Entities.Trucks;
using Microsoft.EntityFrameworkCore;

namespace Coldrun.Trucks.Application.Abstractions.DatabaseContext;

public interface IDatabaseContext
{
    DbSet<Truck> Trucks { get; }
}
