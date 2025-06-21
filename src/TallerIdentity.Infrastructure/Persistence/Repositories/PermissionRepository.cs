using Microsoft.EntityFrameworkCore;
using TallerIdentity.Application.Interfaces.Persistence;
using TallerIdentity.Domain.Entities;
using TallerIdentity.Infrastructure.Persistence.Context;

namespace TallerIdentity.Infrastructure.Persistence.Repositories;

public class PermissionRepository(ApplicationDbContext context) : IPermissionRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> DeleteRolePermission(IEnumerable<RolePermission> rolePermissions)
    {
        _context.RolePermissions.RemoveRange(rolePermissions);

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }

    public async Task<IEnumerable<RolePermission>> GetPermissionRolesByRoleId(int roleId)
    {
        return await _context.RolePermissions
            .AsNoTracking()
            .Where(rp => rp.RoleId == roleId)
            .ToListAsync();
    }

    public async Task<bool> RegisterRolePermissions(IEnumerable<RolePermission> rolePermissions)
    {
        foreach (var rolePermission in rolePermissions)
        {
            rolePermission.State = "1";

            _context.RolePermissions.Add(rolePermission);
        }

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }
}
