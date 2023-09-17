using FcxLabsUserManagement.Application.Common.Models.Auth.Signup;
using FcxLabsUserManagement.Application.Common.ViewModels.User;
using FcxLabsUserManagement.Application.Extensions.Conversions;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Application.User.Queries;
using FcxLabsUserManagement.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] RegisterUser userForm, string role)
    {
        return await _mediator.Send(userForm.ToCreateUserWithRoleCommand(role));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        return await _mediator.Send(new GetUserByIdQuery { Id = id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return await _mediator.Send(new GetAllUsersQuery());
    }

    [HttpGet("filters")]
    public async Task<IActionResult> GetUsersByFilters([FromQuery] UserFiltersVM filters)
    {
        return await _mediator.Send(new GetUsersByFiltersQuery { UserFilters = filters });
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] JsonPatchDocument<UserUpdateVM> userForm, [FromRoute] string id)
    {
        return await _mediator.Send(new UpdateUserCommand { Id = id, PatchedUserUpdate = userForm });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(string id)
    {
        return await _mediator.Send(new DeleteUserCommand { Id = id });
    }
}
