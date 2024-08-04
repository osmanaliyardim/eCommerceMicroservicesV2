using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace eCommerceMicroservicesV2.BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var timer = new Stopwatch();
        
        timer.Start();
        var response = await next();
        timer.Stop();

        var timeTaken = timer.Elapsed.Seconds;
        if (timeTaken > 3)
            logger.LogWarning(Messages.GetPerformanceError<TRequest>(timeTaken));

        return response;
    }
}
