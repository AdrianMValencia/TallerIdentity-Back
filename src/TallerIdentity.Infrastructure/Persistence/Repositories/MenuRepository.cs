using Microsoft.EntityFrameworkCore;
using TallerIdentity.Application.Interfaces.Persistence;
using TallerIdentity.Domain.Entities;
using TallerIdentity.Infrastructure.Persistence.Context;

namespace TallerIdentity.Infrastructure.Persistence.Repositories;

public class MenuRepository(ApplicationDbContext context) : IMenuRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> DeleteMenuRole(IEnumerable<MenuRole> menuRoles)
    {
        _context.MenuRoles.RemoveRange(menuRoles);

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }

    public async Task<IEnumerable<Menu>> GetMenuPermissionAsync()
    {
        var query = _context.Menus
            .AsNoTracking()
            .AsSplitQuery()
            .Where(m => m.Url != null && m.State == "1");

        var menus = await query.ToListAsync();
        return menus;
    }

    public async Task<IEnumerable<MenuRole>> GetMenuRolesByRoleId(int roleId)
    {
        return await _context.MenuRoles
            .AsNoTracking()
            .Where(mr => mr.RoleId == roleId)
            .ToListAsync();
    }

    public async Task<bool> RegisterRoleMenus(IEnumerable<MenuRole> menuRoles)
    {
        foreach (var menuRole in menuRoles)
        {
            menuRole.State = "1";

            _context.MenuRoles.Add(menuRole);
        }

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }
}
