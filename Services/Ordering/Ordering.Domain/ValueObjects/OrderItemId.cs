namespace eCommerceMicroservicesV2.Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }

    private OrderItemId(Guid value) => Value = value;

    public static OrderItemId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        if (value == Guid.Empty)
            throw new DomainException($"{nameof(OrderItemId)} cannot be empty.");

        return new OrderItemId(value);
    }
}
