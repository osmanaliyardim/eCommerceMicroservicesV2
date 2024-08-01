namespace eCommerceMicroservicesV2.Catalog.API.Products.GetProducts;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(IDocumentSession sesion, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        Product productToList = null!;

        try
        {
            productToList = (await sesion.LoadAsync<Product>(query.Id, cancellationToken))!;
        }
        catch (Exception exception)
        {
            logger.LogError("Problem with getting product by id from CatalogDB");

            throw new DatabaseException(exception.Message, exception.StackTrace!);
        }

        if (productToList is null)
        {
            logger.LogError("Products could not found with id criteria");

            throw new ProductNotFoundException(query.Id);
        }
            
        return new GetProductByIdResult(productToList);
    }
}
