using FcxLabsUserManagement.Application.Common.Models.Auth.Signup;
using FcxLabsUserManagement.Application.Extensions.Conversions;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Infra;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly IMediator _mediator;
	
	public AuthController(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	[HttpPost]
	public async Task<IActionResult> Signup([FromBody] RegisterUser userForm)
	{
		return await _mediator.Send(userForm.ToRegisterUserCommand());
	}
	
	[HttpPost]
	public async Task<IActionResult> SignupWithRole([FromBody] RegisterUser userForm, string role)
	{
		return await _mediator.Send(userForm.ToRegisterUserWithRole(role));
	}
}
