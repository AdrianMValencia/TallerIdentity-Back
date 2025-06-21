using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Dtos.Commons;
using TallerIdentity.Application.Dtos.Roles;
using TallerIdentity.Application.UseCases.Roles.Commands.CreateRole;
using TallerIdentity.Application.UseCases.Roles.Commands.DeleteRole;
using TallerIdentity.Application.UseCases.Roles.Commands.UpdateRole;
using TallerIdentity.Application.UseCases.Roles.Queries.GetAllRole;
using TallerIdentity.Application.UseCases.Roles.Queries.GetRoleById;
using TallerIdentity.Application.UseCases.Roles.Queries.GetRoleSelect;

namespace TallerIdentity.Api.Controllers;

[Authorize]
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

    [HttpGet("Select")]
    public async Task<IActionResult> RoleSelect()
    {
        var response = await _dispatcher
            .Dispatch<GetRoleSelectQuery, IEnumerable<SelectResponseDto>>
            (new GetRoleSelectQuery(), CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> RoleCreate([FromBody] CreateRoleCommand command)
    {
        var response = await _dispatcher
            .Dispatch<CreateRoleCommand, bool>(command, CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    public async Task<IActionResult> RoleUpdate([FromBody] UpdateRoleCommand command)
    {
        var response = await _dispatcher
            .Dispatch<UpdateRoleCommand, bool>(command, CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("{roleId:int}")]
    public async Task<IActionResult> RoleDelete(int roleId)
    {
        var response = await _dispatcher
            .Dispatch<DeleteRoleCommand, bool>
            (new DeleteRoleCommand { RoleId = roleId }, CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
