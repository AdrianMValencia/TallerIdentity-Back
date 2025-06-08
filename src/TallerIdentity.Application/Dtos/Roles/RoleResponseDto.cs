namespace TallerIdentity.Application.Dtos.Roles;

public class RoleResponseDto
{
    public int RoleId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? State { get; set; }
    public string? StateDescription { get; set; }
}

public class RoleByIdResponseDto
{
    public int RoleId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? State { get; set; }
}
