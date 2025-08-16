using Microsoft.AspNetCore.Mvc;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Dtos.Permissions;
using TallerIdentity.Application.UseCases.Permissions.Queries.GetPermissionsByRoleId;

namespace TallerIdentity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionController(IDispatcher dispatcher) : ControllerBase
{
    private readonly IDispatcher _dispatcher = dispatcher;

    [HttpGet("PermissionByRoleId/{roleId:int}")]
    public async Task<IActionResult> GetPermissionByRoleId(int roleId, CancellationToken cancellationToken)
    {
        var response = await _dispatcher.Dispatch<GetPermissionsByRoleIdQuery, IEnumerable<PermissionsByRoleResponseDto>>
            (new GetPermissionsByRoleIdQuery() { RoleId = roleId }, cancellationToken);

        return Ok(response);
    }
}
