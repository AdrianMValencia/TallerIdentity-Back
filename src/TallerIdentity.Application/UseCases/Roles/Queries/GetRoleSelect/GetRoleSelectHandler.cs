using Mapster;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Dtos.Commons;
using TallerIdentity.Application.Interfaces.Services;

namespace TallerIdentity.Application.UseCases.Roles.Queries.GetRoleSelect;

internal sealed class GetRoleSelectHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetRoleSelectQuery, IEnumerable<SelectResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(GetRoleSelectQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

        try
        {
            var roles = await _unitOfWork.Role.GetAllAsync();

            if (roles is null)
            {
                response.IsSuccess = false;
                response.Message = "No se encontraron registros.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = roles.Adapt<IEnumerable<SelectResponseDto>>();
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
