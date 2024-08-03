using MediatR;

namespace eCommerceMicroservicesV2.BuildingBlocks.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{

}
