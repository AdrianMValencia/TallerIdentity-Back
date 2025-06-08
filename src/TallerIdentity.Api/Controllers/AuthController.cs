using Microsoft.AspNetCore.Mvc;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.UseCases.Users.Queries.Login;

namespace TallerIdentity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IDispatcher dispatcher) : ControllerBase
{
    private readonly IDispatcher _dispatcher = dispatcher;

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query)
    {
        var response = await _dispatcher.Dispatch<LoginQuery, string>
            (query, CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
