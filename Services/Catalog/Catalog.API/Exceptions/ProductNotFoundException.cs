namespace eCommerceMicroservicesV2.Catalog.API.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(string message) : base(message)
    {
            
    }

    public ProductNotFoundException(Guid Id) : base("Product", Id)
    {

    }
}
