namespace eCommerceMicroservicesV2.Catalog.API.Products.CreateProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult();

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product Id is required");
    }
}

internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        Product productToDelete = null!;

        try
        {
            productToDelete = (await session.LoadAsync<Product>(command.Id, cancellationToken))!;
        }
        catch (Exception exception)
        {
            logger.LogError("Problem with accessing to DB to delete product from CatalogDB");

            throw new DatabaseException(exception.Message, exception.StackTrace!);
        }

        if (productToDelete is null)
        {
            logger.LogError("Product not found to delete");

            throw new ProductNotFoundException(command.Id);
        }

        try
        {
            session.Delete(productToDelete!);
            await session.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError("Problem with deleting product from CatalogDB");

            throw new DatabaseException(exception.Message, exception.StackTrace!);
        }

        return new DeleteProductResult();
    }
}
