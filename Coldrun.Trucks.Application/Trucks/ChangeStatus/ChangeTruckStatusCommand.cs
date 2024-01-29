using Coldrun.Trucks.Domain.Entities.Trucks;
using MediatR;

namespace Coldrun.Trucks.Application.Trucks.ChangeStatus;

public record ChangeTruckStatusCommand(
    Guid TruckId,
    TruckStateTrigger TruckStateTrigger) : IRequest;

