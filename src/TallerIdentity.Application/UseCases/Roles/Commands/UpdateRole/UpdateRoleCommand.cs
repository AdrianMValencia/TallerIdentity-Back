using TallerIdentity.Application.Abstractions.Messaging;

namespace TallerIdentity.Application.UseCases.Roles.Commands.UpdateRole;

public sealed class UpdateRoleCommand : ICommand<bool>
{
    public int RoleId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? State { get; set; }
    public ICollection<PermissionUpdateRequestDto> Permissions { get; set; } = null!;
    public ICollection<MenuUpdateRequestDto> Menus { get; set; } = null!;
}

public class PermissionUpdateRequestDto
{
    public int PermissionId { get; set; }
    public string PermissionName { get; set; } = null!;
    public string PermissionDescription { get; set; } = null!;
    public bool Selected { get; set; }
}

public class MenuUpdateRequestDto
{
    public int MenuId { get; set; }
}