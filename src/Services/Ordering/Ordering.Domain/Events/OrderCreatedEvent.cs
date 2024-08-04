using eCommerceMicroservicesV2.Ordering.Domain.Models;

namespace eCommerceMicroservicesV2.Ordering.Domain.Events;

public record OrderCreatedEvent(Order Order) : IDomainEvent;
