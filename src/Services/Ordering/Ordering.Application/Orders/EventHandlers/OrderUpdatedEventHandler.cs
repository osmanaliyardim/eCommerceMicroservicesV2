namespace eCommerceMicroservicesV2.Ordering.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(Messages.GetDomainEventMessage(notification.GetType().Name));

        return Task.CompletedTask;
    }
}
