using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace eCommerceMicroservicesV2.Basket.API.Data;

public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache distributedCache)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await distributedCache.GetStringAsync(userName, cancellationToken);

        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

        var shoppingCart = await basketRepository.GetBasketAsync(userName, cancellationToken);
        var shoppingCartJson = JsonSerializer.Serialize(shoppingCart);

        await distributedCache.SetStringAsync(userName, shoppingCartJson, cancellationToken);

        return shoppingCart;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        await basketRepository.StoreBasketAsync(shoppingCart, cancellationToken);

        var shoppingCartJson = JsonSerializer.Serialize(shoppingCart);

        await distributedCache.SetStringAsync(shoppingCart.UserName, shoppingCartJson, cancellationToken);

        return shoppingCart;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var shoppingCartJson = await distributedCache.GetStringAsync(userName, cancellationToken);

        if (!string.IsNullOrEmpty(shoppingCartJson))
            await distributedCache.RemoveAsync(userName, cancellationToken);

        var result = await basketRepository.DeleteBasketAsync(userName, cancellationToken);

        return result;
    }
}
