using Mapster;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Dtos.Roles;
using TallerIdentity.Application.Interfaces.Services;

namespace TallerIdentity.Application.UseCases.Roles.Queries.GetRoleById;

internal sealed class GetRoleByIdHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetRoleByIdQuery, RoleByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<RoleByIdResponseDto>> Handle(GetRoleByIdQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<RoleByIdResponseDto>();

		try
		{
            var role = await _unitOfWork.Role.GetByIdAsync(query.RoleId);

            if (role is null)
            {
                response.IsSuccess = false;
                response.Message = "Role no encontrado.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = role.Adapt<RoleByIdResponseDto>();
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
