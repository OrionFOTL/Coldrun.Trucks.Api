using Coldrun.Trucks.Domain.Entities.Primitives;
using Stateless;

namespace Coldrun.Trucks.Domain.Entities.Trucks;

public class Truck(Guid id, AlphanumericString code, string name, string description = "")
{
    public Guid Id { get; private set; } = id;

    public AlphanumericString Code { get; private set; } = code;

    public string Name { get; private set; } = name;

    public TruckStatus Status { get; private set; } = TruckStatus.OutOfService;

    public string Description { get; private set; } = description;

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void ChangeStatus(TruckStateTrigger trigger)
    {
        var stateMachine = GetTruckStateMachine();

        stateMachine.Fire(trigger);
    }

    private StateMachine<TruckStatus, TruckStateTrigger> GetTruckStateMachine()
    {
        var stateMachine = new StateMachine<TruckStatus, TruckStateTrigger>(
            () => Status,
            s => Status = s);

        stateMachine.Configure(TruckStatus.OutOfService)
            .Permit(TruckStateTrigger.StartLoading, TruckStatus.Loading)
            .Permit(TruckStateTrigger.DepartToJob, TruckStatus.ToJob)
            .Permit(TruckStateTrigger.ArriveAtJob, TruckStatus.AtJob)
            .Permit(TruckStateTrigger.StartReturning, TruckStatus.Returning);

        stateMachine.Configure(TruckStatus.Loading)
            .Permit(TruckStateTrigger.DepartToJob, TruckStatus.ToJob)
            .Permit(TruckStateTrigger.LeaveService, TruckStatus.OutOfService);

        stateMachine.Configure(TruckStatus.ToJob)
            .Permit(TruckStateTrigger.ArriveAtJob, TruckStatus.AtJob)
            .Permit(TruckStateTrigger.LeaveService, TruckStatus.OutOfService);

        stateMachine.Configure(TruckStatus.AtJob)
            .Permit(TruckStateTrigger.StartReturning, TruckStatus.Returning)
            .Permit(TruckStateTrigger.LeaveService, TruckStatus.OutOfService);

        stateMachine.Configure(TruckStatus.Returning)
            .Permit(TruckStateTrigger.StartLoading, TruckStatus.Loading)
            .Permit(TruckStateTrigger.LeaveService, TruckStatus.OutOfService);
        
        return stateMachine;
    }
}
