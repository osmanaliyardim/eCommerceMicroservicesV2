namespace eCommerceMicroservicesV2.Ordering.Application.Orders.Commands;

public class DeleteOrderHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.OrderId);
        var orderToDelete = await context.Orders.FindAsync([orderId], cancellationToken);

        if (orderToDelete is null)
            throw new OrderNotFoundException(command.OrderId);

        context.Orders.Remove(orderToDelete);
        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        if (!result)
            throw new DatabaseException(Messages.DATABASE_ERROR);

        return new DeleteOrderResult(result);
    }
}
