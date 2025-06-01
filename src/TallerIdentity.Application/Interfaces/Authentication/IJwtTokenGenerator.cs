using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
