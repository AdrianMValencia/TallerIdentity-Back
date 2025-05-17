using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Infrastructure.Persistence.Context.Configurations;

internal sealed class MenuRoleConfiguration : IEntityTypeConfiguration<MenuRole>
{
    public void Configure(EntityTypeBuilder<MenuRole> builder)
    {
        builder.Ignore(x => x.Id);

        builder.HasKey(x => new { x.MenuId, x.RoleId });
    }
}
