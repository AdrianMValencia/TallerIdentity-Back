using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Dtos.Users;

namespace TallerIdentity.Application.UseCases.Users.Queries.GetAllUser;

public sealed class GetAllUserQuery : BaseFilters, IQuery<IEnumerable<UserResponseDto>>
{
}
