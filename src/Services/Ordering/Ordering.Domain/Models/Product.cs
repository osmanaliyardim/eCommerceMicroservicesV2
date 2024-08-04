namespace eCommerceMicroservicesV2.Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; private set; } = default!;

    public decimal Price { get; private set; } = default!;

    public static Product Create(ProductId id, string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));

        var product = new Product
        {
            Id = id,
            Name = name,
            Price = price
        };

        return product;
    }
}
