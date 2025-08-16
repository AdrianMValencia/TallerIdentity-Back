using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.Interfaces.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> UserByEmailAsync(string email);
    Task<User> UserByEmailAsyncDapper(string email);
}
