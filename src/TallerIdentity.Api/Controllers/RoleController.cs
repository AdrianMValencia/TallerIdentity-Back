using Microsoft.AspNetCore.Mvc;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Dtos.Roles;
using TallerIdentity.Application.UseCases.Roles.Queries.GetAllRole;
using TallerIdentity.Application.UseCases.Roles.Queries.GetRoleById;

namespace TallerIdentity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController(IDispatcher dispatcher) : ControllerBase
{
    private readonly IDispatcher _dispatcher = dispatcher;

    [HttpGet]
    public async Task<IActionResult> RoleList([FromQuery] GetAllRoleQuery query)
    {
        var response = await _dispatcher
            .Dispatch<GetAllRoleQuery, IEnumerable<RoleResponseDto>>(query, CancellationToken.None);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{roleId:int}")]
    public async Task<IActionResult> RoleById(int roleId)
    {
        var response = await _dispatcher
            .Dispatch<GetRoleByIdQuery, RoleByIdResponseDto>
            (new GetRoleByIdQuery { RoleId = roleId }, CancellationToken.None);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
