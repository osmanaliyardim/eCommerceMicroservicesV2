namespace eCommerceMicroservicesV2.Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/bycategories/{categories}",
            async (string categories, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(categories));

                var response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);
            })
        .WithName("GetProductsByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Products By Category")
        .WithDescription("Get Products By Category");
    }
}
