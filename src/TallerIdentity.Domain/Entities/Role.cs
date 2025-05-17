namespace TallerIdentity.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
