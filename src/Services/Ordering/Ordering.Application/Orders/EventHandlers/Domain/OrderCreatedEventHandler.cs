namespace eCommerceMicroservicesV2.Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(Messages.GetDomainEventMessage(notification.GetType().Name));

        return Task.CompletedTask;
    }
}
