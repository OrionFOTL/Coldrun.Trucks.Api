using MediatR;

namespace Coldrun.Trucks.Application.Trucks.Update;

public record UpdateTruckCommand(
    Guid Id,
    string Name,
    string Description) : IRequest;
