namespace eCommerceMicroservicesV2.Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession sesion, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var productsToList = await sesion.Query<Product>().ToListAsync(cancellationToken);

        if (productsToList == null)
            logger.LogError("Problem with getting products from CatalogDB");
 
        return new GetProductsResult(productsToList!);
    }
}
