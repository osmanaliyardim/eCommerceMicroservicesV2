using eCommerceMicroservicesV2.Ordering.Application.Orders.Queries.GetOrderByName;

namespace eCommerceMicroservicesV2.Ordering.API.Endpoints;

public record GetOrdersByNameResponse(IEnumerable<OrderDto> OrderDtos);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", 
            async (string orderName, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByNameQuery(orderName));

                var response = result.Adapt<GetOrdersByNameResponse>();

                return Results.Ok(response);
            }
        )
        .WithName("GetOrdersByName")
        .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Name")
        .WithDescription("Get Orders By Order Name");
    }
}
