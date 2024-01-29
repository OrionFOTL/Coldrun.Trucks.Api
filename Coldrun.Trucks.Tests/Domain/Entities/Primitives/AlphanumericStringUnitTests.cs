using Coldrun.Trucks.Domain.Entities.Primitives;
using FluentAssertions;

namespace Coldrun.Trucks.Tests.Domain.Entities.Primitives;

public class AlphanumericStringUnitTests
{
    [Theory]
    [InlineData("abc")]
    [InlineData("abc123")]
    [InlineData("123abc")]
    [InlineData("123")]
    [InlineData("123abc123")]
    [InlineData("abc123abc")]
    public void AlphanumericString_ForAlphanumericInput_CreatesObject(string alphanumericInput)
    {
        // Act
        var alphanumericString = new AlphanumericString(alphanumericInput);

        // Assert
        alphanumericString.Value.Should().Be(alphanumericInput);
        ((string)alphanumericString).Should().Be(alphanumericInput);
        alphanumericString.ToString().Should().Be(alphanumericInput);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("-")]
    [InlineData("=")]
    [InlineData("@")]
    [InlineData("abc@123")]
    public void AlphanumericString_ForNonAlphanumericInput_ThrowsException(string nonAlphanumericInput)
    {
        // Act
        var action = () => new AlphanumericString(nonAlphanumericInput);

        // Assert
        action.Should().Throw<InvalidAlphanumericStringException>();
    }
}
