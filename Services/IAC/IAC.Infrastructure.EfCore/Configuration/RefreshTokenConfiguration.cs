using IAC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAC.Infrastructure.EfCore.Configuration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.Ignore(rt => rt.IsExpired);
        
        builder.Property(rt => rt.Token).IsRequired();
        
        builder.HasIndex(rt => rt.Token).IsUnique();
        
        builder.Property(rt => rt.Expires).IsRequired();
        
        builder.Property(rt => rt.UserId).IsRequired();
        
        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}