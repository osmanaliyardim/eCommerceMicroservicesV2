namespace eCommerceMicroservicesV2.Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersResult(PaginatedResult<OrderDto> OrderDtos);

public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;
