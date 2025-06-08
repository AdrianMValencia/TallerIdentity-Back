using Mapster;
using TallerIdentity.Application.Dtos.Roles;
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
    }
}
