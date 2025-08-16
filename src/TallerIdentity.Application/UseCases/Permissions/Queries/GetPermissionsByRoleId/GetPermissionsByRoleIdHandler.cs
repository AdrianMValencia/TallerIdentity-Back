using Mapster;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Dtos.Permissions;
using TallerIdentity.Application.Interfaces.Services;

namespace TallerIdentity.Application.UseCases.Permissions.Queries.GetPermissionsByRoleId;

internal sealed class GetPermissionsByRoleIdHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetPermissionsByRoleIdQuery, IEnumerable<PermissionsByRoleResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<IEnumerable<PermissionsByRoleResponseDto>>> Handle(GetPermissionsByRoleIdQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<PermissionsByRoleResponseDto>>();
        var permissionsResult = new List<PermissionsByRoleResponseDto>();

        try
        {
            var menus = await _unitOfWork.Menu.GetMenuPermissionAsync();

            if (menus is null)
            {
                response.IsSuccess = false;
                response.Message = "No se encontraron registros.";
                return response;
            }

            foreach (var menu in menus)
            {
                var permissions = await _unitOfWork.Permission.GetPermissionsByMenuId(menu.Id);

                var dto = new PermissionsByRoleResponseDto
                {
                    MenuId = menu.Id,
                    FatherId = menu.FatherId,
                    Menu = menu.Name,
                    Icon = menu.Icon!,
                    Permissions = permissions.Adapt<List<PermissionsResponseDto>>()
                };

                if (query.RoleId.HasValue)
                {
                    var rolePermissions = await _unitOfWork.Permission.GetRolePermissionsByMenuId(query.RoleId.Value, menu.Id);
                    foreach (var permission in dto.Permissions)
                    {
                        permission.Selected = rolePermissions.Any(rp => rp.Id == permission.PermissionId);
                    }
                }

                permissionsResult.Add(dto);
            }

            response.IsSuccess = true;
            response.Data = permissionsResult;
            response.Message = "Consulta exitosa.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
