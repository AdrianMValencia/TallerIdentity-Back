using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Dtos.Permissions;

namespace TallerIdentity.Application.UseCases.Permissions.Queries.GetPermissionsByRoleId;

public sealed class GetPermissionsByRoleIdQuery : IQuery<IEnumerable<PermissionsByRoleResponseDto>>
{
    public int? RoleId { get; set; }
}
