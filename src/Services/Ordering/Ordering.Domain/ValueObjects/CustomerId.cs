namespace eCommerceMicroservicesV2.Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; }

    private CustomerId(Guid value) => Value = value;

    public static CustomerId Of(Guid value) 
    { 
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        if (value == Guid.Empty)
            throw new DomainException($"{nameof(CustomerId)} cannot be empty.");

        return new CustomerId(value);
    } 
}
