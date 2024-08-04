using eCommerceMicroservicesV2.Ordering.Application.Orders.Queries.GetOrders;

namespace eCommerceMicroservicesV2.Ordering.API.Endpoints;

public record GetOrdersResponse(PaginatedResult<OrderDto> OrderDtos);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders",
            async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersQuery(request));

                var response = result.Adapt<GetOrdersResponse>();

                return Results.Ok(result);
            }
        )
        .WithName("GetOrders")
        .Produces<GetOrdersResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders")
        .WithDescription("Get All Orders");
    }
}
