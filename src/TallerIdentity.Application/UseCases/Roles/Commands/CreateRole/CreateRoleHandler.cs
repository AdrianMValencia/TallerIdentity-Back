using Mapster;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Interfaces.Services;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.UseCases.Roles.Commands.CreateRole;

internal sealed class CreateRoleHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<bool>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        using var transaction = _unitOfWork.BeginTransaction();

        try
		{
            var role = command.Adapt<Role>();
            await _unitOfWork.Role.CreateAsync(role);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var menus = command.Menus
                .Select(menu => new MenuRole
                {
                    RoleId = role.Id,
                    MenuId = menu.MenuId
                })
                .ToList();

            var permissions = command.Permissions
                .Select(permission => new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.PermissionId
                })
                .ToList();

            await _unitOfWork.Permission.RegisterRolePermissions(permissions);
            await _unitOfWork.Menu.RegisterRoleMenus(menus);

            transaction.Commit();
            response.IsSuccess = true;
            response.Message = "Rol creado exitosamente.";
        }
		catch (Exception ex)
		{
            transaction.Rollback();
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
