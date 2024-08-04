namespace eCommerceMicroservicesV2.Ordering.Application.Orders.Queries.GetOrderByName;

public record GetOrdersByNameResult(IEnumerable<OrderDto> OrderDtos);

public record GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameResult>
{

}
