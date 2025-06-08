using TallerIdentity.Application.Abstractions.Messaging;

namespace TallerIdentity.Application.UseCases.UserRoles.Commands.UpdateUserRole;

public sealed class UpdateUserRoleCommand : ICommand<bool>
{
    public int UserRoleId { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public string? State { get; set; } = null!;
}
