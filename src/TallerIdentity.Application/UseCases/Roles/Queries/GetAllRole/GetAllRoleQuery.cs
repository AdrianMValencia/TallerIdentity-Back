using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Dtos.Roles;

namespace TallerIdentity.Application.UseCases.Roles.Queries.GetAllRole;

public class GetAllRoleQuery : BaseFilters, IQuery<IEnumerable<RoleResponseDto>>
{
}
