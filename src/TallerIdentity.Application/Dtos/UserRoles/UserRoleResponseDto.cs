namespace TallerIdentity.Application.Dtos.UserRoles;

public class UserRoleResponseDto
{
    public int UserRoleId { get; set; }
    public string User { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? State { get; set; }
    public string? StateDescription { get; set; }
}

public class UserRoleByIdResponseDto
{
    public int UserRoleId { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public string? State { get; set; }
}