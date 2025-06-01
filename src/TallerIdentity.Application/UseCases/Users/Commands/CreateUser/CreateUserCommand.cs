using TallerIdentity.Application.Abstractions.Messaging;

namespace TallerIdentity.Application.UseCases.Users.Commands.CreateUser;

public sealed class CreateUserCommand : ICommand<bool>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? State { get; set; }
}
