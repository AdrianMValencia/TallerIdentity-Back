namespace TallerIdentity.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public string? State { get; set; }
}
