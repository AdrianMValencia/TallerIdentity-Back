using Mapster;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Interfaces.Services;
using TallerIdentity.Domain.Entities;
using BC = BCrypt.Net.BCrypt;

namespace TallerIdentity.Application.UseCases.Users.Commands.UpdateUser;

internal sealed class UpdateUserHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<UpdateUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<bool>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(
            command,
            () => UpdateUserAsync(command, cancellationToken),
            cancellationToken
        );
    }

    private async Task<BaseResponse<bool>> UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var user = command.Adapt<User>();
            user.Id = command.UserId;
            user.Password = BC.HashPassword(user.Password);
            _unitOfWork.User.Update(user);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Actualización exitosa";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
