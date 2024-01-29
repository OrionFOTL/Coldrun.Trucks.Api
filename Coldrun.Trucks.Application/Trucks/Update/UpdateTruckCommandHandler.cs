using Coldrun.Trucks.Application.UnitOfWork;
using Coldrun.Trucks.Domain.Entities.Trucks;
using MediatR;

namespace Coldrun.Trucks.Application.Trucks.Update;

internal class UpdateTruckCommandHandler(
    IUnitOfWork unitOfWork,
    ITruckRepository truckRepository) : IRequestHandler<UpdateTruckCommand>
{
    public async Task Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
    {
        var truckToUpdate = await truckRepository.GetById(request.Id) ?? throw new TruckNotFoundException(request.Id);

        truckToUpdate.Update(request.Name, request.Description);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
