using Mapster;
using TallerIdentity.Application.Dtos.Commons;
using TallerIdentity.Application.Dtos.Roles;
using TallerIdentity.Application.UseCases.Roles.Commands.CreateRole;
using TallerIdentity.Application.UseCases.Roles.Commands.UpdateRole;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.Mappings;

public class RoleMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Role, RoleResponseDto>()
            .Map(dest => dest.RoleId, src => src.Id)
            .Map(dest => dest.StateDescription, src => src.State == "1" ? "Activo" : "Inactivo")
            .TwoWays();

        config.NewConfig<Role, RoleByIdResponseDto>()
            .Map(dest => dest.RoleId, src => src.Id)
            .TwoWays();

        config.NewConfig<Role, SelectResponseDto>()
            .Map(dest => dest.Code, src => src.Id)
            .Map(dest => dest.Description, src => src.Name)
            .TwoWays();

        config.NewConfig<CreateRoleCommand, Role>();

        config.NewConfig<UpdateRoleCommand, Role>();
    }
}
