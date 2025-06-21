using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.Interfaces.Persistence;

public interface IMenuRepository
{
    Task<bool> RegisterRoleMenus(IEnumerable<MenuRole> menuRoles);
    Task<IEnumerable<MenuRole>> GetMenuRolesByRoleId(int roleId);
    Task<bool> DeleteMenuRole(IEnumerable<MenuRole> menuRoles);
}
