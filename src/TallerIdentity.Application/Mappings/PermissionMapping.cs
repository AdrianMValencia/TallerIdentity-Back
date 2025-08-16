using Mapster;
using TallerIdentity.Application.Dtos.Permissions;
using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.Mappings;

public class PermissionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Permission, PermissionsResponseDto>()
            .Map(dest => dest.PermissionId, src => src.Id)
            .Map(dest => dest.PermissionName, src => src.Name)
            .Map(dest => dest.PermissionDescription, src => src.Description)
            .TwoWays();
    }
}
