using eCommerceMicroservicesV2.Catalog.API.Products.UpdateProduct;
using Microsoft.AspNetCore.Mvc;

public record UpdateProductRequest(Guid Id, string Name, List<string> Categories, string Description,
    string ImageFile, decimal Price);

public record UpdateProductResponse(Product Product);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products",
            async ([FromBody]UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
