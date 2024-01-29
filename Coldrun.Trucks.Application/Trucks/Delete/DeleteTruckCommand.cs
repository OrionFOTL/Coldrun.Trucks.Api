using MediatR;

namespace Coldrun.Trucks.Application.Trucks.Delete;

public record DeleteTruckCommand(Guid TruckId) : IRequest;
