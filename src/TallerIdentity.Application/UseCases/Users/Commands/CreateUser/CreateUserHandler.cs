using Mapster;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Interfaces.Services;
using TallerIdentity.Domain.Entities;
using BC = BCrypt.Net.BCrypt;

namespace TallerIdentity.Application.UseCases.Users.Commands.CreateUser;

internal sealed class CreateUserHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<CreateUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<bool>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(
                command,
                () => CreateUserAsync(command, cancellationToken),
                cancellationToken
            );
    }

    private async Task<BaseResponse<bool>> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var user = command.Adapt<User>();
            user.Password = BC.HashPassword(command.Password);

            await _unitOfWork.User.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = "Registro exitoso.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
