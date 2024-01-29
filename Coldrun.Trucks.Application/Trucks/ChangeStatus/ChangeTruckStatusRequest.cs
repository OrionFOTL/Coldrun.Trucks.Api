using Coldrun.Trucks.Domain.Entities.Trucks;

namespace Coldrun.Trucks.Application.Trucks.ChangeStatus;

public record ChangeTruckStatusRequest(TruckStateTrigger TruckStateTrigger);
