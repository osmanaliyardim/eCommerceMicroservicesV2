namespace eCommerceMicroservicesV2.Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Categories) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession sesion) 
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var categories = query.Categories
            .Split(',')    // "Category A, Category B" => ["Category A", "Category B"]
            .Select(c => c.TrimStart().TrimEnd())    // ["  Category A  ", "   Category B   "] => ["Category A", "Category B"]
            .ToArray();

        if (categories.Length < 1)
            throw new Exception("Category parameters are required");

        IReadOnlyList<Product> productsToList = null!;

        try
        {
            productsToList = await sesion.Query<Product>()
                .Where(product => product.Categories.Any(category => categories.Contains(category)))
                    .ToListAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            throw new DatabaseException(exception.Message, exception.StackTrace!);
        }

        if (productsToList is null)
        {
            throw new ProductNotFoundException(query.Categories);
        }

        return new GetProductByCategoryResult(productsToList);
    }
}
