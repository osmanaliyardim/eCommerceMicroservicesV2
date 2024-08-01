using MediatR;

namespace eCommerceMicroservices2.BuildingBlocks.CQRS;

public interface ICommand : ICommand<Unit>
{

}

public interface ICommand<out TResponse> : IRequest<TResponse>
{

}
