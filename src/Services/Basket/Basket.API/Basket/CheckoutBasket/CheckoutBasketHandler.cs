using eCommerceMicroservicesV2.BuildingBlocks.Messaging.Events;
using MassTransit;

namespace eCommerceMicroservicesV2.Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketChecoutDto BasketCheckoutDto)
    : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto)
            .NotNull()
                .WithMessage("BasketCheckoutDto cannot be null.");

        RuleFor(x => x.BasketCheckoutDto.UserName)
            .NotEmpty()
                .WithMessage("UserName cannot be empty.");
    }
}

public class CheckoutBasketHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await basketRepository
            .GetBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);

        if (basket is null)
            throw new BasketNotFoundException(command.BasketCheckoutDto.UserName);

        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        // Send event message to the RabbitMQ
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        // Remove current basket because checkout is successfull
        await basketRepository.DeleteBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}
