using Microsoft.AspNetCore.Mvc;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Dtos.Commons;
using TallerIdentity.Application.Dtos.Users;
using TallerIdentity.Application.Helpers;
using TallerIdentity.Application.Interfaces.Services;
using TallerIdentity.Application.UseCases.Users.Commands.CreateUser;
using TallerIdentity.Application.UseCases.Users.Commands.DeleteUser;
using TallerIdentity.Application.UseCases.Users.Commands.UpdateUser;
using TallerIdentity.Application.UseCases.Users.Queries.GetAllUser;
using TallerIdentity.Application.UseCases.Users.Queries.GetById;
using TallerIdentity.Application.UseCases.Users.Queries.GetUserSelect;

namespace TallerIdentity.Api.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController(IDispatcher dispatcher, IExcelService excelService, IPdfService pdfService) : ControllerBase
{
    private readonly IDispatcher _dispatcher = dispatcher;
    private readonly IExcelService _excelService = excelService;
    private readonly IPdfService _pdfService = pdfService;

    [HttpGet]
    public async Task<IActionResult> UserList([FromQuery] GetAllUserQuery query)
    {
        var response = await _dispatcher
            .Dispatch<GetAllUserQuery, IEnumerable<UserResponseDto>>(query, CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpGet("Excel")]
    public async Task<IActionResult> UserReportExcel([FromQuery] GetAllUserQuery query)
    {
        var response = await _dispatcher
            .Dispatch<GetAllUserQuery, IEnumerable<UserResponseDto>>(query, CancellationToken.None);

        var columnNames = ReportColumns.GetColumnsUsers();
        var fileBytes = _excelService.GenerateToExcel(response.Data!, columnNames);
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [HttpGet("Pdf")]
    public async Task<IActionResult> UserReportPdf([FromQuery] GetAllUserQuery query)
    {
        var response = await _dispatcher
            .Dispatch<GetAllUserQuery, IEnumerable<UserResponseDto>>(query, CancellationToken.None);

        var columnNames = ReportColumns.GetColumnsUsers();
        var fileBytes = _pdfService.GenerateToPdf(response.Data!, columnNames, "Usuarios");
        return File(fileBytes, "application/pdf");
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> UserById(int userId)
    {
        var response = await _dispatcher
            .Dispatch<GetByIdUserQuery, UserByIdResponseDto>
            (new GetByIdUserQuery { UserId = userId }, CancellationToken.None);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> UserSelect()
    {
        var response = await _dispatcher
            .Dispatch<GetUserSelectQuery, IEnumerable<SelectResponseDto>>
            (new GetUserSelectQuery(), CancellationToken.None);
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
