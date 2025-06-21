using TallerIdentity.Application.Abstractions.Messaging;

namespace TallerIdentity.Application.UseCases.Roles.Commands.DeleteRole;

public sealed class DeleteRoleCommand : ICommand<bool>
{
    public int RoleId { get; set; }
}
