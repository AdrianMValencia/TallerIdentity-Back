using Mapster;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Interfaces.Services;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.UseCases.UserRoles.Commands.UpdateUserRole;

internal sealed class UpdateUserRoleHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<bool>> Handle(UpdateUserRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var userRole = command.Adapt<UserRole>();
            userRole.Id = command.UserRoleId;

            _unitOfWork.UserRole.Update(userRole);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = "Rol de usuario actualizado exitosamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error al actualizar el rol de usuario: {ex.Message}";
        }

        return response;
    }
}
