using Coldrun.Trucks.Application.UnitOfWork;
using Coldrun.Trucks.Domain.Entities.Trucks;
using MediatR;

namespace Coldrun.Trucks.Application.Trucks.Delete;

internal class DeleteTruckCommandHandler(
    IUnitOfWork unitOfWork,
    ITruckRepository truckRepository) : IRequestHandler<DeleteTruckCommand>
{
    public async Task Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
    {
        var truckToDelete = await truckRepository.GetById(request.TruckId);

        if (truckToDelete is null)
        {
            throw new TruckNotFoundException(request.TruckId);
        }

        await truckRepository.Delete(truckToDelete);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
