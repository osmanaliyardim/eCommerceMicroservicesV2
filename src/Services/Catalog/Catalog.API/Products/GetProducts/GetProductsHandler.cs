namespace eCommerceMicroservicesV2.Catalog.API.Products.GetProducts;

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        IPagedList<Product> productsToList = null!;

        try
        {
            productsToList = await session.Query<Product>()
                .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);
        }
        catch (Exception exception)
        {
            throw new DatabaseException(exception.Message, exception.StackTrace!);
        }

        if (productsToList is null)
        {
            throw new ProductNotFoundException("No Product(s) to List");
        }

        return new GetProductsResult(productsToList);
    }
}
