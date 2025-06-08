using Mapster;
using TallerIdentity.Application.Dtos.UserRoles;
using TallerIdentity.Application.UseCases.UserRoles.Commands.CreateUserRole;
using TallerIdentity.Application.UseCases.UserRoles.Commands.UpdateUserRole;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.Mappings;

public class UserRoleMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserRole, UserRoleResponseDto>()
            .Map(dest => dest.UserRoleId, src => src.Id)
            .Map(dest => dest.User, src => src.User.FirstName + " " + src.User.LastName)
            .Map(dest => dest.Role, src => src.Role.Name)
            .Map(dest => dest.StateDescription, src => src.State == "1" ? "Activo" : "Inactivo")
            .TwoWays();


        config.NewConfig<UserRole, UserRoleByIdResponseDto>()
           .Map(dest => dest.UserRoleId, src => src.Id)
           .TwoWays();


        config.NewConfig<CreateUserRoleCommand, UserRole>();
        config.NewConfig<UpdateUserRoleCommand, UserRole>();
    }
}
