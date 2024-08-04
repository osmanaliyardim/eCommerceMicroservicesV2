namespace eCommerceMicroservicesV2.Ordering.Application.Orders.Commands;

public record CreateOrderResult(OrderDto OrderDto);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.OrderDto.OrderName)
            .NotEmpty()
                .WithMessage("Name is required.");

        RuleFor(x => x.OrderDto.CustomerId)
            .NotNull()
                .WithMessage("CustomerId is required.");

        RuleFor(x => x.OrderDto.OrderItems)
            .NotEmpty()
                .WithMessage("OrderItems should not be empty.");
    }
}

public record CreateOrderCommand(OrderDto OrderDto) : ICommand<CreateOrderResult>;
