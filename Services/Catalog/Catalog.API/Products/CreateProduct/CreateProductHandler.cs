using eCommerceMicroservices2.BuildingBlocks.CQRS;
using eCommerceMicroservicesV2.Catalog.API.Models;

namespace eCommerceMicroservicesV2.Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name, List<string> Categories, string Description, 
    string ImageFile, decimal Price
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid id);

internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
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

        // ToDo: save to DB

        return new CreateProductResult(Guid.NewGuid());
    }
}
