namespace eCommerceMicroservicesV2.Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name, List<string> Categories, string Description,
    string ImageFile, decimal Price
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3).MaximumLength(50)
            .WithMessage("Product Name must include characters between 3 - 50.")
            .NotEmpty()
            .WithMessage("Product Name is required");
        RuleFor(x => x.Categories)
            .NotEmpty()
            .WithMessage("At least one Category is required for Product.");
        RuleFor(x => x.ImageFile)
            .NotEmpty()
            .WithMessage("Product ImageFile is required.");
        RuleFor(x => x.Price).GreaterThan(0).LessThan(100001)
            .WithMessage("Product Price must be between 1 - 100000.")
            .NotEmpty()
            .WithMessage("Product Price is required.");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productToCreate = new Product
        {
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        try
        {
            session.Store(productToCreate);
            await session.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError("Problem with saving product to CatalogDB");

            throw new DatabaseException(exception.Message, exception.StackTrace!);
        }

        return new CreateProductResult(productToCreate.Id);
    }
}
