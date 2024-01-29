using Coldrun.Trucks.Domain.Entities.Trucks;

namespace Coldrun.Trucks.Persistence.Repositories;

internal class TruckRepository(DatabaseContext databaseContext) : ITruckRepository
{
    public async Task<Truck?> GetById(Guid id) => await databaseContext.Trucks.FindAsync(id);

    public async Task Add(Truck truck) => await databaseContext.Trucks.AddAsync(truck);

    public Task Delete(Truck truck) => Task.FromResult(databaseContext.Trucks.Remove(truck));
}
