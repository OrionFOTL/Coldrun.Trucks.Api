using MediatR;

namespace Coldrun.Trucks.Application.Trucks.Get.Single;

public record GetTruckQuery(Guid TruckId) : IRequest<GetTruckResponse>;
