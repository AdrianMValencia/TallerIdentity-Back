using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Dtos.UserRoles;

namespace TallerIdentity.Application.UseCases.UserRoles.Queries.GetAllUserRole;

public sealed class GetAllUserRoleQuery : BaseFilters, IQuery<IEnumerable<UserRoleResponseDto>>
{
}
