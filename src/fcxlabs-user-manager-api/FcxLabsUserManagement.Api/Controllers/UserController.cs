using FcxLabsUserManagement.Application.Common.Models.Auth.Signup;
using FcxLabsUserManagement.Application.Extensions.Conversions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
	
	[HttpPost("add")]
	public async Task<IActionResult> CreateUserAsync([FromBody] RegisterUser userForm, string role)
	{
		return await _mediator.Send(userForm.ToCreateUserWithRoleCommand(role));
	}
}
