using MediatR;

namespace eCommerceMicroservices2.BuildingBlocks.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{

}
