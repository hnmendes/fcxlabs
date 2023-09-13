using FcxLabsUserManagement.Application.Common.Models.Auth.Signup;
using FcxLabsUserManagement.Application.Extensions.Conversions;
using FcxLabsUserManagement.Application.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
	private readonly IMediator _mediator;

	public AuthController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("sign-up")]
	public async Task<IActionResult> Signup([FromBody] RegisterUser userForm)
	{
		return await _mediator.Send(userForm.ToRegisterUserCommand(Request.Scheme, Url));
	}
	
	[HttpGet("confirm-email")]
	public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
	{
		return await _mediator.Send(new ConfirmUserEmailCommand { Token = token, Email = email });
	}
}
