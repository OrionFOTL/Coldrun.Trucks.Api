using System.Linq.Expressions;
using Coldrun.Trucks.Application.Abstractions.DatabaseContext;
using Coldrun.Trucks.Application.Trucks.Get.Single;
using Coldrun.Trucks.Domain.Entities.Trucks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coldrun.Trucks.Application.Trucks.Get.Many.Rest;

internal class GetFilteredTrucksQueryHandler(
    IDatabaseContext databaseContext) : IRequestHandler<GetFilteredTrucksQuery, List<GetTruckResponse>>
{
    public async Task<List<GetTruckResponse>> Handle(GetFilteredTrucksQuery request, CancellationToken cancellationToken)
    {
        var trucksQuery = databaseContext
            .Trucks
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.TextSearchTerm))
        {
            trucksQuery = trucksQuery.Where(
                t => ((string)t.Code).Contains(request.TextSearchTerm)
                  || t.Name.Contains(request.TextSearchTerm)
                  || t.Description.Contains(request.TextSearchTerm));
        }

        trucksQuery = request.SortOrder?.ToLowerInvariant() == "desc"
            ? trucksQuery.OrderByDescending(GetOrderByExpression(request))
            : trucksQuery.OrderBy(GetOrderByExpression(request));

        var trucks = await trucksQuery
            .Select(t => new GetTruckResponse(
                t.Id,
                t.Code.Value,
                t.Name,
                t.Status,
                t.Description))
            .ToListAsync(cancellationToken);

        return trucks;
    }

    private static Expression<Func<Truck, object>> GetOrderByExpression(GetFilteredTrucksQuery request)
    {
        return request.SortColumn?.ToLowerInvariant() switch
        {
            "code" => truck => truck.Code,
            "name" => truck => truck.Name,
            "status" => truck => truck.Status,
            "description" => truck => truck.Description,
            _ => truck => truck.Id,
        };
    }
}
