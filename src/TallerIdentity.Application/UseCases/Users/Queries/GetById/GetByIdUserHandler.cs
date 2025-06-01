using Mapster;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Dtos.Users;
using TallerIdentity.Application.Interfaces.Services;

namespace TallerIdentity.Application.UseCases.Users.Queries.GetById;

internal sealed class GetByIdUserHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetByIdUserQuery, UserByIdResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<UserByIdResponseDto>> Handle(GetByIdUserQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserByIdResponseDto>();

		try
		{
            var user = await _unitOfWork.User.GetByIdAsync(query.UserId);

            if (user is null)
            {
                response.IsSuccess = false;
                response.Message = "No se encontraron registros.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = user.Adapt<UserByIdResponseDto>();
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
