using Coldrun.Trucks.Application.Trucks.Get.Single;
using Coldrun.Trucks.Domain.Entities.Trucks;
using MediatR;

namespace Coldrun.Trucks.Application.Trucks.Get.Many.Rest;

public record GetFilteredTrucksQuery(
    string? TextSearchTerm,
    TruckStatus? TruckStatusSearchTerm,
    string? SortColumn,
    string? SortOrder) : IRequest<List<GetTruckResponse>>;
