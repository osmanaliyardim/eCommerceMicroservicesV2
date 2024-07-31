namespace eCommerceMicroservicesV2.Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("Product not found!")
    {
        
    }
}
