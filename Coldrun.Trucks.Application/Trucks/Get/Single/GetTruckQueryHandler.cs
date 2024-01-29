using Coldrun.Trucks.Application.Abstractions.DatabaseContext;
using Coldrun.Trucks.Domain.Entities.Trucks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coldrun.Trucks.Application.Trucks.Get.Single;

internal class GetTruckQueryHandler(IDatabaseContext databaseContext) : IRequestHandler<GetTruckQuery, GetTruckResponse>
{
    public async Task<GetTruckResponse> Handle(GetTruckQuery request, CancellationToken cancellationToken)
    {
        var truck = await databaseContext
            .Trucks
            .AsNoTracking()
            .Where(t => t.Id == request.TruckId)
            .Select(t => new GetTruckResponse(
                t.Id,
                t.Code.Value,
                t.Name,
                t.Status,
                t.Description))
            .FirstOrDefaultAsync(cancellationToken);

        if (truck is null)
        {
            throw new TruckNotFoundException(request.TruckId);
        }

        return truck;
    }
}
