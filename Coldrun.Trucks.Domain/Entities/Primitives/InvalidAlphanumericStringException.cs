namespace Coldrun.Trucks.Domain.Entities.Primitives;

internal class InvalidAlphanumericStringException(string inputString)
    : Exception($"The input string '{inputString}' was not alphanumeric");
