namespace Coldrun.Trucks.Domain.Entities.Primitives;

[Serializable]
internal class InvalidAlphanumericStringException : Exception
{
    public InvalidAlphanumericStringException(string inputString)
        : base($"The input string '{inputString}' was not alphanumeric")
    {
    }
}