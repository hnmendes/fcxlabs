using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.Extensions;
using FcxLabsUserManagement.Application.Resources;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts.Services;
using FcxLabsUserManagement.Infra.Configurations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class RegisterUserHandler: IRequestHandler<RegisterUserCommand, ObjectResult>
{
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
	private readonly EmailConfig _emailConfig;

	public RegisterUserHandler(IUserService userService, IEmailService emailService, EmailConfig emailConfig)
	{
        _userService = userService;
        _emailService = emailService;
		_emailConfig = emailConfig;
	}

	public async Task<ObjectResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		if(!request.CPF.IsCPFValid())
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User CPF is not valid." })
			{
				StatusCode = StatusCodes.Status422UnprocessableEntity	
			};
		}
		
		if(!request.Password.IsPasswordValid())
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User Password doesn't meet with one of the following criterias: Special Character, Upper Character." })
			{
				StatusCode = StatusCodes.Status422UnprocessableEntity	
			};
		}
		
		var userExists = await _userService.GetUserByEmailAsync(request.Email);
		
		if(userExists is not null)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User already exists!" })
			{
				StatusCode = StatusCodes.Status403Forbidden
			};
		}
		
		UserIdentity user = new()
		{
			Email = request.Email,
			BirthDate = request.BirthDate,
			CPF = request.CPF,
			MotherName = request.MotherName,
			Name = request.Name,
			MobilePhone = request.MobilePhone,
			SecurityStamp = Guid.NewGuid().ToString(),
			Status = Core.Enums.Status.ACTIVATED,
			UserName = request.UserName
		};
		
		var result = await _userService.CreateUserAsync(user, request.Password);
		
		if(!result)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User failed to be created." })
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};	
		}
		
		var token = await _userService.GenerateEmailConfirmationTokenAsync(user);
		var confirmationLink = UrlHelperExtensions.Action(request.Url, "ConfirmEmail", "Auth", new { token = token, email = user.Email }, request.Scheme);
		var message = new Message(new string [] { user.Email }, "Confirmação de Email - FcxLabs", String.Format(EmailTemplates.ConfirmEmailTemplate, confirmationLink, _emailConfig.Email));
		
		await _emailService.SendEmailAsync(message);
		
		return new ObjectResult(new Response { Status = "Success", Message = "User created successfully!" })
		{
			StatusCode = StatusCodes.Status201Created
		};
	}
}
