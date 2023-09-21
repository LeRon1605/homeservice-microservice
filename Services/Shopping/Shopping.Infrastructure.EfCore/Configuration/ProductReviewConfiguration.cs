using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.ProductAggregate;

namespace Shopping.Infrastructure.EfCore.Configuration;

public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Rating)
               .IsRequired()
               .HasDefaultValue(0);

        builder.Property(x => x.Description)
               .IsRequired();
        
        builder.HasOne<Product>()
               .WithMany(x => x.Reviews)
               .HasForeignKey(x => x.ProductId);
    }
}