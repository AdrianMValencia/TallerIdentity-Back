using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Infrastructure.Persistence.Context.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("RoleId");

        builder.Property(x => x.Name)
            .HasMaxLength(50);

        builder.Property(x => x.Description)
           .HasMaxLength(100);
    }
}
