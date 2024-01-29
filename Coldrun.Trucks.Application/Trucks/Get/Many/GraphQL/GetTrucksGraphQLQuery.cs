using Coldrun.Trucks.Application.Abstractions.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Coldrun.Trucks.Application.Trucks.Get.Many.GraphQL;

public class GetTrucksGraphQLQuery
{
    [GraphQLName("trucks")]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<GetTrucksGraphQLResponse> GetTrucks([Service(ServiceKind.Synchronized)] IDatabaseContext databaseContext)
    {
        return databaseContext
            .Trucks
            .AsNoTracking()
            .Select(t => new GetTrucksGraphQLResponse
            {
                Id = t.Id,
                Code = t.Code.Value,
                Name = t.Name,
                Status = t.Status,
                Description = t.Description
            });
    }
}
