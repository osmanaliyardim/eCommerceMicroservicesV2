namespace eCommerceMicroservicesV2.Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardName { get; } = default!;

    public string CardNumber { get; } = default!;

    public string Expiration { get; } = default!;

    public string CVV { get; } = default!;

    public int PaymentMethod { get; } = default!;

    protected Payment()
    {

    }

    private Payment
        (string? cardName, string cardNumber, string expiration, 
        string cVV, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cVV;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string? cardName, string cardNumber, string expiration,
        string cvv, int paymentMethod)
    {
        ArgumentException
            .ThrowIfNullOrWhiteSpace(cardName, nameof(cardName));
        ArgumentException
            .ThrowIfNullOrWhiteSpace(cardNumber, nameof(cardNumber));
        ArgumentException
            .ThrowIfNullOrWhiteSpace(cvv, nameof(cvv));
        ArgumentOutOfRangeException
            .ThrowIfGreaterThan(cvv.Length, 3, nameof(cvv));
        ArgumentOutOfRangeException
            .ThrowIfLessThan(cvv.Length, 3, nameof(cvv));

        return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
    }
}
