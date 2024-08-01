namespace eCommerceMicroservicesV2.Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Product> productsToList = null!;

        try
        {
            productsToList = await session.Query<Product>().ToListAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError("Problem with getting products from CatalogDB");

            throw new DatabaseException(exception.Message, exception.StackTrace!);
        }

        if (productsToList is null)
        {
            logger.LogInformation("Products could not found");

            throw new ProductNotFoundException("No Product(s) to List");
        }

        return new GetProductsResult(productsToList);
    }
}
