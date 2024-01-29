using Coldrun.Trucks.Domain.Entities.Trucks;

namespace Coldrun.Trucks.Application.Trucks.Get.Many.GraphQL;

public class GetTrucksGraphQLResponse
{
    public Guid? Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public TruckStatus? Status { get; set; }

    public string? Description { get; set; }
}
