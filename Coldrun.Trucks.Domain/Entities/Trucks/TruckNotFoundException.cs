namespace Coldrun.Trucks.Domain.Entities.Trucks;

public class TruckNotFoundException(Guid id)
    : Exception($"The truck with an id of '{id}' was not found");
