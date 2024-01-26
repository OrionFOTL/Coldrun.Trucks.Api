using System.Text.RegularExpressions;

namespace Coldrun.Trucks.Domain.Entities.Primitives;

public record AlphanumericString
{
    private static readonly Regex _alphanumericRegex = new(@"^[a-zA-Z0-9]+$");

    public AlphanumericString(string inputString)
    {
        if (!_alphanumericRegex.IsMatch(inputString))
        {
            throw new InvalidAlphanumericStringException(inputString);
        }

        AlphanumericString = inputString;
    }

    public string AlphanumericString { get; private set; }
}
