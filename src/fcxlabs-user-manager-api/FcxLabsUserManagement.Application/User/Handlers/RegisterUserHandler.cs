using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.Resources;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Core.Contracts.Services;
using FcxLabsUserManagement.Infra;
using FcxLabsUserManagement.Infra.Configurations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class RegisterUserHandler: IRequestHandler<RegisterUserCommand, ObjectResult>
{	
	private readonly UserManager<UserIdentity> _userManager;
	private readonly IEmailService _emailService;
    private readonly EmailConfig _emailConfig;

    public RegisterUserHandler(UserManager<UserIdentity> userManager, IEmailService emailService, EmailConfig emailConfig)
	{
		_userManager = userManager;
		_emailService = emailService;
        _emailConfig = emailConfig;
    }

	public async Task<ObjectResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		if(!IsCPFValid(request.CPF))
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User CPF is not valid." })
			{
				StatusCode = StatusCodes.Status422UnprocessableEntity	
			};
		}
		
		if(!IsPasswordValid(request.Password))
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User Password doesn't meet with one of the following criterias: Special Character, Upper Character." })
			{
				StatusCode = StatusCodes.Status422UnprocessableEntity	
			};
		}
		
		var userExists = await _userManager.FindByEmailAsync(request.Email);
		
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
		
		var result = await _userManager.CreateAsync(user, request.Password);
		
		if(!result.Succeeded)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User failed to be created." })
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};	
		}
		
		var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
		var confirmationLink = UrlHelperExtensions.Action(request.Url, "ConfirmEmail", "Auth", new { token = token, email = user.Email }, request.Scheme);
		var message = new Message(new string [] { user.Email }, "Confirmação de Email - FcxLabs", String.Format(EmailTemplates.ConfirmEmailTemplate, confirmationLink, _emailConfig.Email));
		
		await _emailService.SendEmailAsync(message);
		
		return new ObjectResult(new Response { Status = "Success", Message = "User created successfully!" })
		{
			StatusCode = StatusCodes.Status201Created
		};
	}
	
	private static bool IsPasswordValid(string password)
	{
		bool containsNonAlphanumeric = password.Any(char.IsLetterOrDigit);
		bool containsUpperCase = password.Any(char.IsUpper);
		bool isValid = containsNonAlphanumeric && containsUpperCase;
		return isValid;
	}

	private static bool IsCPFValid(string strCPF)
	{
		string value = strCPF.Replace(".", "");
		value = value.Replace("-", "");
		if (value.Length != 11)
			return false;

		bool isEqual = true;

		for (int i = 1; i < 11 && isEqual; i++)
			if (value[i] != value[0])
				isEqual = false;

		if (isEqual || value == "12345678909")
			return false;

		int[] numbers = new int[11];

		for (int i = 0; i < 11; i++)
			numbers[i] = int.Parse(
			value[i].ToString());

		int sum = 0;

		for (int i = 0; i < 9; i++)
			sum += (10 - i) * numbers[i];

		int result = sum % 11;

		if (result == 1 || result == 0)
		{
			if (numbers[9] != 0)
				return false;
		}

		else if (numbers[9] != 11 - result)
			return false;

		sum = 0;

		for (int i = 0; i < 10; i++)
			sum += (11 - i) * numbers[i];

		result = sum % 11;

		if (result == 1 || result == 0)
		{
			if (numbers[10] != 0)
				return false;
		}
		else
			if (numbers[10] != 11 - result)
				return false;

		return true;
	}
}
