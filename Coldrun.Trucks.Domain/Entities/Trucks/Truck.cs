using Coldrun.Trucks.Domain.Entities.Primitives;

namespace Coldrun.Trucks.Domain.Entities.Trucks;

public class Truck
{
    public required Guid Id { get; init; }

    public required AlphanumericString Code { get; set; }

    public required string Name { get; set; }

    public required TruckStatus Status { get; set; }

    public string Description { get; set; } = string.Empty;
}
