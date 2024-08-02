namespace eCommerceMicroservicesV2.Basket.API.Basket.GetBasket;

public record GetBasketQuery(string Id) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart ShoppingCart);

internal class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
