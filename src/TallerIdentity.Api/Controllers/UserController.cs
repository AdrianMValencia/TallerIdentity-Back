﻿using Microsoft.AspNetCore.Mvc;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Dtos.Users;
using TallerIdentity.Application.UseCases.Users.Commands.CreateUser;
using TallerIdentity.Application.UseCases.Users.Commands.DeleteUser;
using TallerIdentity.Application.UseCases.Users.Commands.UpdateUser;
using TallerIdentity.Application.UseCases.Users.Queries.GetAllUser;
using TallerIdentity.Application.UseCases.Users.Queries.GetById;

namespace TallerIdentity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IDispatcher dispatcher) : ControllerBase
{
    private readonly IDispatcher _dispatcher = dispatcher;

    [HttpGet]
    public async Task<IActionResult> UserList([FromQuery] GetAllUserQuery query)
    {
        var response = await _dispatcher
            .Dispatch<GetAllUserQuery, IEnumerable<UserResponseDto>>(query, CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> UserById(int userId)
    {
        var response = await _dispatcher
            .Dispatch<GetByIdUserQuery, UserByIdResponseDto>
            (new GetByIdUserQuery { UserId = userId }, CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> UserCreate([FromBody] CreateUserCommand command)
    {
        var response = await _dispatcher.Dispatch<CreateUserCommand, bool>
            (command, CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    public async Task<IActionResult> UserUpdate([FromBody] UpdateUserCommand command)
    {
        var response = await _dispatcher.Dispatch<UpdateUserCommand, bool>
            (command, CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("{userId:int}")]
    public async Task<IActionResult> UserDelete(int userId)
    {
        var response = await _dispatcher
            .Dispatch<DeleteUserCommand, bool>
            (new DeleteUserCommand { UserId = userId }, CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
