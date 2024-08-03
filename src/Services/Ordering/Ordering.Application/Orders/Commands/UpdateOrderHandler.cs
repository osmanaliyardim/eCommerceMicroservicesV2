namespace eCommerceMicroservicesV2.Ordering.Application.Orders.Commands;

public class UpdateOrderHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.OrderDto.Id);
        var orderToUpdate = await context.Orders.FindAsync([orderId], cancellationToken);

        if (orderToUpdate is null)
            throw new OrderNotFoundException(command.OrderDto.Id);

        UpdateOrder(orderToUpdate, command.OrderDto);

        context.Orders.Update(orderToUpdate);
        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        if (!result)
            throw new DatabaseException(Messages.DATABASE_ERROR);

        return new UpdateOrderResult(command.OrderDto);
    }
    
    private void UpdateOrder(Order orderToUpdate, OrderDto orderDto)
    {
        var updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,
                       orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine,
                       orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State,
                       orderDto.ShippingAddress.ZipCode);

        var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName,
                       orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine,
                       orderDto.BillingAddress.Country, orderDto.BillingAddress.State,
                       orderDto.BillingAddress.ZipCode);

        var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber,
                       orderDto.Payment.Expiration, orderDto.Payment.Cvv,
                       orderDto.Payment.PaymentMethod);

        orderToUpdate.Update(
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: updatedShippingAddress,
            billingAddress: updatedBillingAddress,
            payment: updatedPayment,
            status: orderDto.Status
        );
    }
}
