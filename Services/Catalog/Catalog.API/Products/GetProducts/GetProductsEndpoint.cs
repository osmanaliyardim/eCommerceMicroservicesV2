namespace eCommerceMicroservicesV2.Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",
            async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());

                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            }
        )
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
