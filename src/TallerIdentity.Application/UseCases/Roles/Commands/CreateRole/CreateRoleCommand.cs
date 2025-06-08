using TallerIdentity.Application.Abstractions.Messaging;

namespace TallerIdentity.Application.UseCases.Roles.Commands.CreateRole;

public class CreateRoleCommand : ICommand<bool>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? State { get; set; }
    public ICollection<PermissionRequestDto> Permissions { get; set; } = null!;
    public ICollection<MenuRequestDto> Menus { get; set; } = null!;
}

public class PermissionRequestDto
{
    public int PermissionId { get; set; }
    public string PermissionName { get; set; } = null!;
    public string PermissionDescription { get; set; } = null!;
    public bool Selected { get; set; }
}

public class MenuRequestDto
{
    public int MenuId { get; set; }
}