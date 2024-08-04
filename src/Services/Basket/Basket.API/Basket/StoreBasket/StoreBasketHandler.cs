using eCommerceMicroservicesV2.Discount.Grpc;

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

internal class StoreBasketCommandHandler(IBasketRepository basketRepository,
    DiscountProtoService.DiscountProtoServiceClient discountGrpcClient) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        // Use Discount.Grpc to check if there is any discount for this basket
        await CalculateDiscountAsync(command.ShoppingCart, cancellationToken);

        await basketRepository.StoreBasketAsync(command.ShoppingCart, cancellationToken);

        return new StoreBasketResult(command.ShoppingCart);
    }

    public async Task CalculateDiscountAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken)
    {
        foreach (var item in shoppingCart.Items)
        {
            var couponModel = await discountGrpcClient.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
            item.Price -= couponModel.Amount;
        }
    }
}
