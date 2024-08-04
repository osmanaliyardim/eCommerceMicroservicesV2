using Marten.Schema;

namespace eCommerceMicroservicesV2.Basket.API.Data;

public class BasketInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = await store.LightweightSerializableSessionAsync(cancellation);

        if (await session.Query<ShoppingCart>().AnyAsync())
            return;

        // Marten UpSert will insert or update the existing record(s)
        session.Store<ShoppingCart>(GetPreconfiguredBaskets());

        await session.SaveChangesAsync();
    }

    private static IEnumerable<ShoppingCart> GetPreconfiguredBaskets() => new List<ShoppingCart>()
    {
        new ShoppingCart()
        {
            UserName = "osmanaliyardim",
            Items = new List<ShoppingCartItem>()
            {
                new ShoppingCartItem()
                {
                    ProductId = new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
                    ProductName = "Samsung 10",
                    Color = "Blue",
                    Price = 840.00M,
                    Quantity = 3
                },
                new ShoppingCartItem()
                {
                    ProductId = new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
                    ProductName = "Samsung 10",
                    Color = "Violet",
                    Price = 840.00M,
                    Quantity = 5
                },
                new ShoppingCartItem()
                {
                    ProductId = new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"),
                    ProductName = "Xiaomi Mi 9",
                    Color = "Red",
                    Price = 470.00M,
                    Quantity = 1
                },
                new ShoppingCartItem()
                {
                    ProductId = new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"),
                    ProductName = "Xiaomi Mi 9",
                    Color = "Blue",
                    Price = 470.00M,
                    Quantity = 2
                }
            }
        }
    };
}
