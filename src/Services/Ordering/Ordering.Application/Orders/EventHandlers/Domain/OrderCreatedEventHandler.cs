using eCommerceMicroservicesV2.Ordering.Application.Extensions;
using MassTransit;
using Microsoft.FeatureManagement;

namespace eCommerceMicroservicesV2.Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler
    (IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        var domainEventName = domainEvent.GetType().Name;

        logger.LogInformation(Messages.GetDomainEventMessage(domainEventName));

        if (await featureManager.IsEnabledAsync(Messages.ORDER_FULLFILMENT_FLAG))
        {
            var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();

            // Send event message to the RabbitMQ
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
        else logger.LogWarning(Messages.GetDomainEventFailedMessage(domainEventName));
    }
}
