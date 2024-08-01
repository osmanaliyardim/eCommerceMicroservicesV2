using eCommerceMicroservices2.BuildingBlocks.CQRS;
using FluentValidation;

namespace eCommerceMicroservices2.BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = 
            await Task.WhenAll(validators.Select(val => val.ValidateAsync(context, cancellationToken)));

        var errors = validationResults
            .Where(vr => vr.Errors.Any())
                .SelectMany(vr => vr.Errors)
                    .ToList();

        if (errors.Any())
            throw new ValidationException(errors);

        return await next();
    }
}
