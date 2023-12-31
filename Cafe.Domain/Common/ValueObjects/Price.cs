using Cafe.Domain.Common.Models;

namespace Cafe.Domain.Common.ValueObjects;

public sealed class Price : ValueObject
{
    Price(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; private set; }

    public string Currency { get; private set; }

    public static Price Create(decimal amount, string currency) => 
        new(amount, currency);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}