using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Infrastructure.Persistence.Context.Configurations;

internal sealed class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("MenuId");

        builder.Property(x => x.Name)
            .HasMaxLength(20);

        builder.Property(x => x.Icon)
           .HasMaxLength(25);

        builder.Property(x => x.Url)
           .HasMaxLength(150);
    }
}
