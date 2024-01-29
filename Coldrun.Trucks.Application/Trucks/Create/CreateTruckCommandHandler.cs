using Coldrun.Trucks.Application.UnitOfWork;
using Coldrun.Trucks.Domain.Entities.Primitives;
using Coldrun.Trucks.Domain.Entities.Trucks;
using MediatR;

namespace Coldrun.Trucks.Application.Trucks.Create;

internal class CreateTruckCommandHandler(
    IUnitOfWork unitOfWork,
    ITruckRepository truckRepository) : IRequestHandler<CreateTruckCommand, Guid>
{
    public async Task<Guid> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
    {
        var newTruck = new Truck(
            Guid.NewGuid(),
            new AlphanumericString(request.Code),
            request.Name,
            request.Description);

        await truckRepository.Add(newTruck);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newTruck.Id;
    }
}
