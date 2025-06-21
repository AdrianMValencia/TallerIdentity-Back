using Mapster;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Dtos.Commons;
using TallerIdentity.Application.Interfaces.Services;

namespace TallerIdentity.Application.UseCases.Users.Queries.GetUserSelect;

internal sealed class GetUserSelectHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetUserSelectQuery, IEnumerable<SelectResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(GetUserSelectQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

        try
        {
            var users = await _unitOfWork.User.GetAllAsync();

            if (users is null)
            {
                response.IsSuccess = false;
                response.Message = "No se encontraron registros.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = users.Adapt<IEnumerable<SelectResponseDto>>();
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
