namespace Coldrun.Trucks.Domain.Entities.Trucks;

public interface ITruckRepository
{
    Task<Truck?> GetById(Guid id);

    Task Add(Truck truck);

    Task Delete(Truck truck);
}
