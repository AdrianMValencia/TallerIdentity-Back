using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using TallerIdentity.Application.Interfaces.Persistence;
using TallerIdentity.Application.Interfaces.Services;
using TallerIdentity.Domain.Entities;
using TallerIdentity.Infrastructure.Persistence.Context;

namespace TallerIdentity.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IGenericRepository<Role> Role { get; }
    public IGenericRepository<UserRole> UserRole { get; }
    public IUserRepository User { get; }
    public IPermissionRepository Permission { get; }
    public IMenuRepository Menu { get; }

    public UnitOfWork(
        ApplicationDbContext context,
        IGenericRepository<Role> roleRepository,
        IGenericRepository<UserRole> userRoleRepository,
        IUserRepository user,
        IPermissionRepository permission,
        IMenuRepository menu)
    {
        _context = context;
        Role = roleRepository;
        UserRole = userRoleRepository;
        User = user;
        Permission = permission;
        Menu = menu;
    }

    public IDbTransaction BeginTransaction() =>
        _context.Database.BeginTransaction().GetDbTransaction();

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);

    public void Dispose() => _context.Dispose();
}
