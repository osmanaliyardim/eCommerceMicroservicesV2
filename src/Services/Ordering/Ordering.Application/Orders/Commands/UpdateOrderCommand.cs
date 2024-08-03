namespace eCommerceMicroservicesV2.Ordering.Application.Orders.Commands;

public record UpdateOrderResult(OrderDto OrderDto);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderDto.Id)
            .NotEmpty()
                .WithMessage("Id is required.");

        RuleFor(x => x.OrderDto.OrderName)
            .NotEmpty()
                .WithMessage("Name is required.");

        RuleFor(x => x.OrderDto.CustomerId)
            .NotNull()
                .WithMessage("CustomerId is required.");
    }
}

public record UpdateOrderCommand(OrderDto OrderDto) : ICommand<UpdateOrderResult>
{

}
