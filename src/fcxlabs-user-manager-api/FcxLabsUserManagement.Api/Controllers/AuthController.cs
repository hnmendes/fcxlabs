using FcxLabsUserManagement.Application.Common.Models.Auth.Signup;
using FcxLabsUserManagement.Application.Common.ViewModels;
using FcxLabsUserManagement.Application.Common.ViewModels.Auth.Login;
using FcxLabsUserManagement.Application.Extensions.Conversions;
using FcxLabsUserManagement.Application.User.Commands;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly IValidator<RegisterUser> _registerUserValidator;

	public AuthController(IMediator mediator, IValidator<RegisterUser> registerUserValidator)
	{
		_mediator = mediator;
		_registerUserValidator = registerUserValidator;
	}

	[HttpPost("sign-up")]
	public async Task<IActionResult> Signup([FromBody] RegisterUser userForm)
	{
		var validationResult = await _registerUserValidator.ValidateAsync(userForm);
		
		if(!validationResult.IsValid)
		{
			return StatusCode(StatusCodes.Status400BadRequest, new Error{ Errors = validationResult.ToDictionary() });
		}
		
		return await _mediator.Send(userForm.ToRegisterUserCommand(Request.Scheme, Url));
	}
	
	[HttpGet("confirm-email")]
	public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
	{
		return await _mediator.Send(new ConfirmUserEmailCommand { Token = token, Email = email });
	}
	
	[HttpPost("log-in")]
	public async Task<IActionResult> Login([FromBody] LoginVM loginForm)
	{
		return await _mediator.Send(loginForm.ToLoginUserCommand());
	}
}
