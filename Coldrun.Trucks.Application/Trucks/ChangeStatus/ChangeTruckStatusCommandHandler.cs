using Coldrun.Trucks.Application.UnitOfWork;
using Coldrun.Trucks.Domain.Entities.Trucks;
using MediatR;

namespace Coldrun.Trucks.Application.Trucks.ChangeStatus;

internal class ChangeTruckStatusCommandHandler(
    IUnitOfWork unitOfWork,
    ITruckRepository truckRepository) : IRequestHandler<ChangeTruckStatusCommand>
{
    public async Task Handle(ChangeTruckStatusCommand request, CancellationToken cancellationToken)
    {
        var truckToUpdate = await truckRepository.GetById(request.TruckId) ?? throw new TruckNotFoundException(request.TruckId);

        truckToUpdate.ChangeStatus(request.TruckStateTrigger);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
