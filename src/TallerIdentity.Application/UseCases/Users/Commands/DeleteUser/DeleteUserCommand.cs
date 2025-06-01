using TallerIdentity.Application.Abstractions.Messaging;

namespace TallerIdentity.Application.UseCases.Users.Commands.DeleteUser;

public sealed class DeleteUserCommand : ICommand<bool>
{
    public int UserId { get; set; }
}
