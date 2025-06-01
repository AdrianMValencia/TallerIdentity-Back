using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Interfaces.Services;

namespace TallerIdentity.Application.UseCases.Users.Commands.DeleteUser;

internal sealed class DeleteUserHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<bool>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existsUser = await _unitOfWork.User.GetByIdAsync(command.UserId);

            if (existsUser is null)
            {
                response.IsSuccess = false;
                response.Message = "El usuario no existe en la base de datos.";
                return response;
            }

            await _unitOfWork.User.DeleteAsync(command.UserId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = "Eliminación exitosa.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
