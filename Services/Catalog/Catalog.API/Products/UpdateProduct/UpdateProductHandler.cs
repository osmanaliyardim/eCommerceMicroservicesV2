namespace eCommerceMicroservicesV2.Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description,
    string ImageFile, decimal Price
) : ICommand<UpdateProductResult>;

public record UpdateProductResult(Product Product);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product Id is required");
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

public class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        Product productToUpdate = null!;

        try
        {
            productToUpdate = (await session.LoadAsync<Product>(command.Id, cancellationToken))!;
        }
        catch (Exception exception)
        {
            throw new DatabaseException(exception.Message, exception.StackTrace!);
        }

        if (productToUpdate is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        productToUpdate.Name = command.Name;
        productToUpdate.Description = command.Description;
        productToUpdate.ImageFile = command.ImageFile;
        productToUpdate.Price = command.Price;
        productToUpdate.Categories = command.Categories;

        try
        {
            session.Update(productToUpdate);
            await session.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            throw new DatabaseException(exception.Message, exception.StackTrace!);
        }

        return new UpdateProductResult(productToUpdate);
    }
}
