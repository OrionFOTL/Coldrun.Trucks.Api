using MediatR;

namespace Coldrun.Trucks.Application.Trucks.Create;

public record CreateTruckCommand(
    string Code,
    string Name,
    string Description) : IRequest<Guid>;
