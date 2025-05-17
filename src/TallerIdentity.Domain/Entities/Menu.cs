namespace TallerIdentity.Domain.Entities;

public class Menu : BaseEntity
{
    public int Position { get; set; }
    public string Name { get; set; } = null!;
    public string? Icon { get; set; }
    public string? Url { get; set; }
    public int? FatherId { get; set; }
}
