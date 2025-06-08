using TallerIdentity.Application.Abstractions.Messaging;

namespace TallerIdentity.Application.UseCases.Users.Queries.Login;

public class LoginQuery : IQuery<string>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
