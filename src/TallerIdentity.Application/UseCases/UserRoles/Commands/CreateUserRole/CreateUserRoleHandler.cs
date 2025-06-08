using Mapster;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Interfaces.Services;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.UseCases.UserRoles.Commands.CreateUserRole;

internal sealed class CreateUserRoleHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateUserRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<bool>> Handle(CreateUserRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var userRole = command.Adapt<UserRole>();
            await _unitOfWork.UserRole.CreateAsync(userRole);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = "Rol de usuario creado exitosamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error al crear el rol de usuario: {ex.Message}";
        }

        return response;
    }
}
