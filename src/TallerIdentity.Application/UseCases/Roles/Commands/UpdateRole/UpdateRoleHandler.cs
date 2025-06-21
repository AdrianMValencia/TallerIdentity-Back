using Mapster;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Interfaces.Services;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.UseCases.Roles.Commands.UpdateRole;

internal sealed class UpdateRoleHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<bool>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var role = command.Adapt<Role>();
            role.Id = command.RoleId;

            _unitOfWork.Role.Update(role);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var existingPermissions = await _unitOfWork.Permission
                .GetPermissionRolesByRoleId(command.RoleId);

            var existingMenus = await _unitOfWork.Menu
                .GetMenuRolesByRoleId(command.RoleId);

            var newPermissions = command.Permissions
                .Where(p => p.Selected && !existingPermissions.Any(ep => ep.PermissionId == p.PermissionId))
                .Select(p => new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = p.PermissionId
                });

            await _unitOfWork.Permission.RegisterRolePermissions(newPermissions);

            var newMenus = command.Menus
                .Where(m => !existingMenus.Any(em => em.MenuId == m.MenuId))
                .Select(m => new MenuRole
                {
                    RoleId = role.Id,
                    MenuId = m.MenuId
                });

            await _unitOfWork.Menu.RegisterRoleMenus(newMenus);

            var permissionsToDelete = existingPermissions
                .Where(ep => !command.Permissions.Any(p => p.PermissionId == ep.PermissionId && p.Selected))
                .ToList();

            await _unitOfWork.Permission.DeleteRolePermission(permissionsToDelete);

            var menusToDelete = existingMenus
                .Where(em => !command.Menus.Any(m => m.MenuId == em.MenuId))
                .ToList();

            await _unitOfWork.Menu.DeleteMenuRole(menusToDelete);

            transaction.Commit();
            response.IsSuccess = true;
            response.Message = "Rol actualizado exitosamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
