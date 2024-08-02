namespace eCommerceMicroservicesV2.Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(ShoppingCart ShoppingCart);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart)
            .NotEmpty()
            .WithMessage("Basket is required");
        RuleFor(x => x.ShoppingCart.UserName)
            .MinimumLength(3).MaximumLength(50)
            .WithMessage("UserName must include between 3 - 50 characters.")
            .NotEmpty()
            .WithMessage("UserName is required");
    }
}

internal class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.ShoppingCart;

        throw new NotImplementedException();
    }
}
