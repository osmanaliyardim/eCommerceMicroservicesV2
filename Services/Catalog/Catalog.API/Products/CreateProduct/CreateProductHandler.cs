namespace eCommerceMicroservicesV2.Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name, List<string> Categories, string Description,
    string ImageFile, decimal Price
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session)
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

        session.Store(productToCreate);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(productToCreate.Id);
    }
}
