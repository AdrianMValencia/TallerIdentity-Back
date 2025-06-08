using TallerIdentity.Application.Abstractions.Messaging;

namespace TallerIdentity.Application.UseCases.UserRoles.Commands.DeleteUserRole;

public sealed class DeleteUserRoleCommand : ICommand<bool>
{
    public int UserRoleId { get; set; }
}
