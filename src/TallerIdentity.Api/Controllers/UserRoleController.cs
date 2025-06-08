using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Dtos.UserRoles;
using TallerIdentity.Application.UseCases.UserRoles.Commands.CreateUserRole;
using TallerIdentity.Application.UseCases.UserRoles.Commands.DeleteUserRole;
using TallerIdentity.Application.UseCases.UserRoles.Commands.UpdateUserRole;
using TallerIdentity.Application.UseCases.UserRoles.Queries.GetAllUserRole;
using TallerIdentity.Application.UseCases.UserRoles.Queries.GetById;

namespace TallerIdentity.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserRoleController(IDispatcher dispatcher) : ControllerBase
{
    private readonly IDispatcher _dispatcher = dispatcher;

    [HttpGet]
    public async Task<IActionResult> UserRoleList([FromQuery] GetAllUserRoleQuery query)
    {
        var response = await _dispatcher
            .Dispatch<GetAllUserRoleQuery, IEnumerable<UserRoleResponseDto>>(query, CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{userRoleId:int}")]
    public async Task<IActionResult> UserRoleById(int userRoleId)
    {
        var response = await _dispatcher
            .Dispatch<GetUserRoleByIdQuery, UserRoleByIdResponseDto>
            (new GetUserRoleByIdQuery { UserRoleId = userRoleId }, CancellationToken.None);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> UserRoleCreate([FromBody] CreateUserRoleCommand command)
    {
        var response = await _dispatcher.Dispatch<CreateUserRoleCommand, bool>
            (command, CancellationToken.None);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    public async Task<IActionResult> UserRoleUpdate([FromBody] UpdateUserRoleCommand command)
    {
        var response = await _dispatcher.Dispatch<UpdateUserRoleCommand, bool>
            (command, CancellationToken.None);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("{userRoleId:int}")]
    public async Task<IActionResult> UserRoleDelete(int userRoleId)
    {
        var response = await _dispatcher
            .Dispatch<DeleteUserRoleCommand, bool>
            (new DeleteUserRoleCommand { UserRoleId = userRoleId }, CancellationToken.None);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
