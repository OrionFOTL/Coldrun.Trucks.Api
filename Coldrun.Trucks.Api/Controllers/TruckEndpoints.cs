using Coldrun.Trucks.Application.Trucks.ChangeStatus;
using Coldrun.Trucks.Application.Trucks.Create;
using Coldrun.Trucks.Application.Trucks.Delete;
using Coldrun.Trucks.Application.Trucks.Get.Many.Rest;
using Coldrun.Trucks.Application.Trucks.Get.Single;
using Coldrun.Trucks.Application.Trucks.Update;
using Coldrun.Trucks.Domain.Entities.Trucks;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Coldrun.Trucks.Api.Controllers;

public static class TruckEndpoints
{
    public static IEndpointRouteBuilder MapTruckEndpoints(this IEndpointRouteBuilder builder)
    {
        var trucks = builder.MapGroup("/trucks");

        trucks.MapGet("/{id:guid}", GetTruck);
        trucks.MapGet("/", GetFilteredTrucks);
        trucks.MapPost("/", CreateTruck);
        trucks.MapPost("/{id:guid}/status", ChangeTruckStatus);
        trucks.MapPut("/{id:guid}", UpdateTruck);
        trucks.MapDelete("/{id:guid}", DeleteTruck);

        return builder;
    }

    internal static async Task<Results<Ok<GetTruckResponse>, NotFound<string>>> GetTruck(Guid id, ISender sender)
    {
        try
        {
            var truck = await sender.Send(new GetTruckQuery(id));

            return TypedResults.Ok(truck);
        }
        catch (TruckNotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
    }

    internal static async Task<Ok<List<GetTruckResponse>>> GetFilteredTrucks(
        string? textSearchTerm,
        TruckStatus? truckStatusSearchTerm,
        string? sortColumn,
        string? sortOrder,
        ISender sender)
    {
        var query = new GetFilteredTrucksQuery(textSearchTerm, truckStatusSearchTerm, sortColumn, sortOrder);

        var trucks = await sender.Send(query);

        return TypedResults.Ok(trucks);
    }

    internal static async Task<Created<Guid>> CreateTruck(CreateTruckCommand command, ISender sender)
    {
        var newTruckGuid = await sender.Send(command);

        return TypedResults.Created($"/trucks/{newTruckGuid}", newTruckGuid);
    }

    internal static async Task<NoContent> UpdateTruck(Guid id, UpdateTruckRequest request, ISender sender)
    {
        var updateCommand = new UpdateTruckCommand(id, request.Name, request.Description);

        await sender.Send(updateCommand);

        return TypedResults.NoContent();
    }

    internal static async Task<Results<Ok, Conflict<string>>> ChangeTruckStatus(Guid id, ChangeTruckStatusRequest request, ISender sender)
    {
        var changeStatusCommand = new ChangeTruckStatusCommand(id, request.TruckStateTrigger);

        try
        {
            await sender.Send(changeStatusCommand);

            return TypedResults.Ok();
        }
        catch (InvalidOperationException e)
        {
            return TypedResults.Conflict(e.Message);
        }
    }

    internal static async Task<Results<NoContent, NotFound<string>>> DeleteTruck(Guid id, ISender sender)
    {
        try
        {
            await sender.Send(new DeleteTruckCommand(id));

            return TypedResults.NoContent();
        }
        catch (TruckNotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
    }
}
