using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Dtos.Users;

namespace TallerIdentity.Application.UseCases.Users.Queries.GetById;

public sealed class GetByIdUserQuery : IQuery<UserByIdResponseDto>
{
    public int UserId { get; set; }
}
