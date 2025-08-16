using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.Interfaces.Persistence;

public interface IPermissionRepository
{
    Task<bool> RegisterRolePermissions(IEnumerable<RolePermission> rolePermissions);
    Task<IEnumerable<RolePermission>> GetPermissionRolesByRoleId(int roleId);
    Task<bool> DeleteRolePermission(IEnumerable<RolePermission> rolePermissions);
    Task<IEnumerable<Permission>> GetPermissionsByMenuId(int menuId);
    Task<IEnumerable<Permission>> GetRolePermissionsByMenuId(int roleId, int menuId);
}
