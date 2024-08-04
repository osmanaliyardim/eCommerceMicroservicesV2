namespace eCommerceMicroservicesV2.Ordering.Application.Orders.Commands;

public record DeleteOrderResult(bool IsSuccess);

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
                .WithMessage("OrderId is required.");
    }
}

public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;
