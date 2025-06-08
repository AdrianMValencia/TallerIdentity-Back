using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Interfaces.Authentication;
using TallerIdentity.Application.Interfaces.Services;
using BC = BCrypt.Net.BCrypt;

namespace TallerIdentity.Application.UseCases.Users.Queries.Login;

internal sealed class LoginHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator) 
    : IQueryHandler<LoginQuery, string>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<BaseResponse<string>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<string>();

        try
        {
            var user = await _unitOfWork.User.UserByEmailAsync(query.Email);

            if (user is null)
            {
                response.IsSuccess = false;
                response.Message = "El usuario no existe en la base de datos.";
                return response;
            }

            if (!BC.Verify(query.Password, user.Password))
            {
                response.IsSuccess = false;
                response.Message = "La contraseña es incorrecta.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = _jwtTokenGenerator.GenerateToken(user);
            response.Message = "Token generado correctamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
