namespace eCommerceMicroservicesV2.Catalog.API.Exceptions;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string userName) : base(nameof(ShoppingCart), userName)
    {

    }
}
