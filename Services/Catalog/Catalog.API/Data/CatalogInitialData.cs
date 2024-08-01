using Marten.Schema;

namespace eCommerceMicroservicesV2.Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = await store.LightweightSerializableSessionAsync(cancellation);

        if (await session.Query<Product>().AnyAsync())
            return;

        // Marten UpSert will insert or update the existing record(s)
        session.Store<Product>(GetPreconfiguredProducts());

        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
    {
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "IPhone X",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-1.png",
            Price = 950.00M,
            Categories = new List<string> { "Smart Phone" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Samsung 10",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-2.png",
            Price = 840.00M,
            Categories = new List<string> { "Smart Phone" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Huawei Plus",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-3.png",
            Price = 650.00M,
            Categories = new List<string> { "White Appliances" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Xiaomi Mi 9",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-4.png",
            Price = 470.00M,
            Categories = new List<string> { "White Appliances" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "HTC U11+ Plus",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-5.png",
            Price = 380.00M,
            Categories = new List<string> { "Smart Phone" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "LG G7 ThinQ",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-6.png",
            Price = 240.00M,
            Categories = new List<string> { "Home Kitchen" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Panasonic Lumix",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-6.png",
            Price = 240.00M,
            Categories = new List<string> { "Camera" }
        }
    };
}
