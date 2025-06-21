using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Interfaces.Services;

namespace TallerIdentity.Application.UseCases.Roles.Commands.DeleteRole;

internal sealed class DeleteRoleHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<bool>> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var role = await _unitOfWork.Role.GetByIdAsync(command.RoleId);

            if (role is null)
            {
                response.IsSuccess = false;
                response.Message = "Rol no encontrado.";
                return response;
            }

            await _unitOfWork.Role.DeleteAsync(command.RoleId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            response.IsSuccess = true;
            response.Message = "Rol eliminado exitosamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
