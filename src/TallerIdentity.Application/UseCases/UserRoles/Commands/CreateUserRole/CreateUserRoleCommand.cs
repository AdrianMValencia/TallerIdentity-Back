using TallerIdentity.Application.Abstractions.Messaging;

namespace TallerIdentity.Application.UseCases.UserRoles.Commands.CreateUserRole;

public class CreateUserRoleCommand : ICommand<bool>
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public string? State { get; set; } = null!;
}
