using Coldrun.Trucks.Api.Controllers;
using Coldrun.Trucks.Application.Trucks.ChangeStatus;
using Coldrun.Trucks.Application.Trucks.Delete;
using Coldrun.Trucks.Application.Trucks.Get.Single;
using Coldrun.Trucks.Domain.Entities.Trucks;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Coldrun.Trucks.Tests.Api.Controllers;

public class TruckEndpointsUnitTests()
{
    private readonly Guid _truckId = Guid.NewGuid();
    private readonly ISender _sender = A.Fake<ISender>();

    [Fact]
    public async void GetTruck_WhenTruckExists_Returns200OK()
    {
        // Arrange
        var returnedTruck = new GetTruckResponse(_truckId, "a", "a", TruckStatus.OutOfService, "a");

        A.CallTo(() => _sender.Send(new GetTruckQuery(_truckId), A<CancellationToken>._)).Returns(returnedTruck);

        // Act
        var result = await TruckEndpoints.GetTruck(_truckId, _sender);

        // Assert
        result.Result.Should().BeOfType<Ok<GetTruckResponse>>()
            .Which.Value.Should().Be(returnedTruck);
    }

    [Fact]
    public async void GetTruck_WhenTruckDoesntExist_Returns404NotFound()
    {
        // Arrange
        var thrownException = new TruckNotFoundException(_truckId);

        A.CallTo(() => _sender.Send(new GetTruckQuery(_truckId), A<CancellationToken>._)).Throws(thrownException);

        // Act
        var result = await TruckEndpoints.GetTruck(_truckId, _sender);

        // Assert
        result.Result.Should().BeOfType<NotFound<string>>()
            .Which.Value.Should().Be(thrownException.Message);
    }

    [Fact]
    public async void ChangeTruckStatus_WhenSuccessful_Returns200OK()
    {
        // Arrange
        A.CallTo(() => _sender.Send(A<ChangeTruckStatusCommand>._, A<CancellationToken>._)).DoesNothing();

        // Act
        var result = await TruckEndpoints.ChangeTruckStatus(_truckId, new ChangeTruckStatusRequest(TruckStateTrigger.StartLoading), _sender);

        // Assert
        result.Result.Should().BeOfType<Ok>();
    }

    [Fact]
    public async void ChangeTruckStatus_WhenInvalidOperationException_Returns409Conflict()
    {
        // Arrange
        var thrownException = new InvalidOperationException("Conflict occurred");

        A.CallTo(() => _sender.Send(A<ChangeTruckStatusCommand>._, A<CancellationToken>._)).Throws(thrownException);

        // Act
        var result = await TruckEndpoints.ChangeTruckStatus(_truckId, new ChangeTruckStatusRequest(TruckStateTrigger.StartLoading), _sender);

        // Assert
        result.Result.Should().BeOfType<Conflict<string>>()
            .Which.Value.Should().Be(thrownException.Message);
    }

    [Fact]
    public async void DeleteTruck_WhenSuccessful_Returns204NoContent()
    {
        // Arrange
        A.CallTo(() => _sender.Send(A<DeleteTruckCommand>._, A<CancellationToken>._)).DoesNothing();

        // Act
        var result = await TruckEndpoints.DeleteTruck(_truckId, _sender);

        // Assert
        result.Result.Should().BeOfType<NoContent>();
    }

    [Fact]
    public async void DeleteTruck_WhenTruckNotFound_Returns404NotFound()
    {
        // Arrange
        var thrownException = new TruckNotFoundException(_truckId);

        A.CallTo(() => _sender.Send(A<DeleteTruckCommand>._, A<CancellationToken>._)).Throws(thrownException);

        // Act
        var result = await TruckEndpoints.DeleteTruck(_truckId, _sender);

        // Assert
        result.Result.Should().BeOfType<NotFound<string>>()
            .Which.Value.Should().Be(thrownException.Message);
    }
}
