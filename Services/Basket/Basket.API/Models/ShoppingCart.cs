namespace eCommerceMicroservicesV2.Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;

    public List<ShoppingCartItem> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    // Required for Mapping
    public ShoppingCart()
    {
        
    }
}
