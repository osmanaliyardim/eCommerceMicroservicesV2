using eCommerceMicroservicesV2.Ordering.Application.Extensions;

namespace eCommerceMicroservicesV2.Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var customerId = CustomerId.Of(query.CustomerId);

        var orders = await context.Orders
            .Include(o => o.OrderItems)
                .AsNoTracking()
                    .Where(o => o.CustomerId == customerId)
                        .OrderBy(o => o.OrderName.Value)
                            .ToListAsync(cancellationToken);

        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
    }
}
