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

        Value = inputString;
    }

    public string Value { get; private set; }

    public override string ToString() => Value;

    public static explicit operator string(AlphanumericString inputString) => inputString.Value;
}
