namespace TallerIdentity.Application.Dtos.Commons;

public record SelectResponseDto
{
    public string? Code { get; init; }
    public string? Description { get; init; }
}
