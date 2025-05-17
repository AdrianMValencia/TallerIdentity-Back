namespace TallerIdentity.Domain.Entities;

public class Permission : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Slug { get; set; } = null!;
    public int MenuId { get; set; }
    public Menu Menu { get; set; } = null!;
}
