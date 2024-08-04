using eCommerceMicroservicesV2.Ordering.Application.Extensions;
using MassTransit;

namespace eCommerceMicroservicesV2.Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler
    (IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation(Messages.GetDomainEventMessage(domainEvent.GetType().Name));

        var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();

        // Send event message to the RabbitMQ
        await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
    }
}
