using Mapster;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Dtos.UserRoles;
using TallerIdentity.Application.Interfaces.Services;

namespace TallerIdentity.Application.UseCases.UserRoles.Queries.GetById;

internal sealed class GetUserRoleByIdHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetUserRoleByIdQuery, UserRoleByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<UserRoleByIdResponseDto>> Handle
        (GetUserRoleByIdQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserRoleByIdResponseDto>();

        try
        {
            var userRole = await _unitOfWork.UserRole
                .GetByIdAsync(query.UserRoleId);

            if (userRole is null)
            {
                response.IsSuccess = false;
                response.Message = "No se encontró el rol de usuario.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = userRole.Adapt<UserRoleByIdResponseDto>();
            response.Message = "Consulta exitosa.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error al consultar el rol de usuario: {ex.Message}";
        }
        return response;
    }
}
