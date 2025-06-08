using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Interfaces.Services;

namespace TallerIdentity.Application.UseCases.UserRoles.Commands.DeleteUserRole;

internal sealed class DeleteUserRoleHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<bool>> Handle(DeleteUserRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var userRole = await _unitOfWork.UserRole.GetByIdAsync(command.UserRoleId);

            if (userRole is null)
            {
                response.IsSuccess = false;
                response.Message = "Rol de usuario no encontrado.";
                return response;
            }

            await _unitOfWork.UserRole.DeleteAsync(command.UserRoleId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = "Rol de usuario eliminado exitosamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error al eliminar el rol de usuario: {ex.Message}";
        }

        return response;
    }
}
