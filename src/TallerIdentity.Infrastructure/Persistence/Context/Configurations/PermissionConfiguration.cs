using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Infrastructure.Persistence.Context.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("PermissionId");

        builder.Property(x => x.Name)
            .HasMaxLength(150);

        builder.Property(x => x.Description)
           .HasMaxLength(255);
    }
}
