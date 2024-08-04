using eCommerceMicroservicesV2.Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace eCommerceMicroservicesV2.Ordering.API.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> CustomerDtos);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/bycustomer/{customerId}", 
            async (Guid customerId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));

                var response = result.Adapt<GetOrdersByCustomerResponse>();

                return Results.Ok(response);
            }
        )
        .WithName("GetOrdersByCustomer")
        .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Customer")
        .WithDescription("Get Orders By CustomerId");
    }
}
