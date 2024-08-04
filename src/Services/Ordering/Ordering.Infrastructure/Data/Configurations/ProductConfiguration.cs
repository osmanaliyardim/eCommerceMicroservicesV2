using eCommerceMicroservicesV2.Ordering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceMicroservicesV2.Ordering.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion
            (
                productId => productId.Value,
                dbId => ProductId.Of(dbId)
            );

        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
    }
}
