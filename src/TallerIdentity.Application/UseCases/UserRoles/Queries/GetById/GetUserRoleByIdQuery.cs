using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Dtos.UserRoles;

namespace TallerIdentity.Application.UseCases.UserRoles.Queries.GetById;

public sealed class GetUserRoleByIdQuery : IQuery<UserRoleByIdResponseDto>
{
    public int UserRoleId { get; set; }
}
