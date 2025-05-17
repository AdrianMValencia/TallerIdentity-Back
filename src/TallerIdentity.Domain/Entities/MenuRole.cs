namespace TallerIdentity.Domain.Entities;

public class MenuRole : BaseEntity
{
    public int MenuId { get; set; }
    public int RoleId { get; set; }
    public Menu Menu { get; set; } = null!;
    public Role Role { get; set; } = null!;
}
