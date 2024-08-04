namespace eCommerceMicroservicesV2.Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerResult(IEnumerable<OrderDto> CustomerDtos);

public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerResult>;
