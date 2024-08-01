namespace eCommerceMicroservicesV2.Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Categories) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession sesion, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var categories = query.Categories
            .Split(',')    // "Category A, Category B" => ["Category A", "Category B"]
            .Select(c => c.TrimStart().TrimEnd())    // ["  Category A  ", "   Category B   "] => ["Category A", "Category B"]
            .ToArray();

        var productsToList = await sesion.Query<Product>()
            .Where(product => product.Categories.Any(category => categories.Contains(category)))
            .ToListAsync(cancellationToken);

        if (productsToList is null)
        {
            logger.LogError("Problem with getting products by category from CatalogDB");

            throw new ProductNotFoundException();
        }

        return new GetProductByCategoryResult(productsToList);
    }
}
