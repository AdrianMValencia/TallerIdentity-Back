using Mapster;
using TallerIdentity.Application.Dtos.Users;
using TallerIdentity.Application.UseCases.Users.Commands.CreateUser;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.Mappings;

public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserResponseDto>()
            .Map(dest => dest.UserId, src => src.Id)
            .Map(dest => dest.StateDescription, src => src.State == "1" ? "Activo" : "Inactivo")
            .TwoWays();

        config.NewConfig<User, UserByIdResponseDto>()
            .Map(dest => dest.UserId, src => src.Id)
            .TwoWays();

        config.NewConfig<CreateUserCommand, User>();
    }
}
