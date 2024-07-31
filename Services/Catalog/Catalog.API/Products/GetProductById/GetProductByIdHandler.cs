namespace eCommerceMicroservicesV2.Catalog.API.Products.GetProducts;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(IDocumentSession sesion, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var productToList = await sesion.LoadAsync<Product>(query.Id, cancellationToken);

        if (productToList is null)
        {
            logger.LogError("Problem with getting product from CatalogDB");

            throw new ProductNotFoundException();
        }
            
        return new GetProductByIdResult(productToList);
    }
}
