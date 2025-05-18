using System.Data;
using TallerIdentity.Application.Interfaces.Persistence;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.Interfaces.Services;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Role> Role { get; }
    IGenericRepository<UserRole> UserRole { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IDbTransaction BeginTransaction();
}
