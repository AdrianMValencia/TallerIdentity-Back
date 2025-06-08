using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Dtos.Roles;

namespace TallerIdentity.Application.UseCases.Roles.Queries.GetRoleById;

public sealed class GetRoleByIdQuery : IQuery<RoleByIdResponseDto>
{
    public int RoleId { get; set; }
}
