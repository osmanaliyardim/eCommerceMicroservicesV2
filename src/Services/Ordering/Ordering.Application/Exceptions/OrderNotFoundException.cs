namespace eCommerceMicroservicesV2.Ordering.Application.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid id) : base(nameof(Order), id)
    {
        
    }
}
