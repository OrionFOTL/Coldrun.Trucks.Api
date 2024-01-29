using Coldrun.Trucks.Domain.Entities.Trucks;

namespace Coldrun.Trucks.Application.Trucks.Get.Single;

public record GetTruckResponse(
    Guid Id,
    string Code,
    string Name,
    TruckStatus Status,
    string Description);
