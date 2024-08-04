using eCommerceMicroservicesV2.Ordering.Domain.Enums;

namespace eCommerceMicroservicesV2.Ordering.Application.Dtos;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus Status,
    List<OrderItemDto> OrderItems
);
