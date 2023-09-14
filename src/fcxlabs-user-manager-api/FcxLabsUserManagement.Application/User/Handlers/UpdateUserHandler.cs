using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.Extensions;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ObjectResult>
{
	private readonly IUserService _userService;

	public UpdateUserHandler(IUserService userService)
	{
		_userService = userService;
	}
	
	public async Task<ObjectResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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
		
		var user = await _userService.GetUserByEmailAsync(request.Email);
		
		if(user is null)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User failed to update!" })
			{
				StatusCode = StatusCodes.Status404NotFound
			};
		}
		
		await UpdateUserName(user, request);	
		await UpdateCPF(user, request);
		await UpdateEmail(user, request);
		await UpdatePassword(user, request);
        UpdateFieldsWithoutValidation(user, request);
		var result = await _userService.UpdateUserAsync(user);
		
		if(!result)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User failed to be created." })
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};	
		}
		
		return new ObjectResult(new Response { Status = "Success", Message = "User updated successfully!" })
		{
			StatusCode = StatusCodes.Status201Created
		};
	}
	
	private async Task UpdateUserName(UserIdentity user, UpdateUserCommand request)
	{
		var userNameAlreadyExists = await _userService.AlreadyHasUserName(request.UserName);
		
		if(user.UserName != request.UserName && !userNameAlreadyExists)
		{
			user.UserName = request.UserName;
		}
	}
	
	private async Task UpdateCPF(UserIdentity user, UpdateUserCommand request)
	{
		var cpfAlreadyExists = await _userService.AlreadyHasCPF(request.CPF);
		
		if(user.CPF != request.UserName && !cpfAlreadyExists)
		{
			user.CPF = request.CPF;
		}
	}
	
	private async Task UpdateEmail(UserIdentity user, UpdateUserCommand request)
	{
		var emailAlreadyExists = await _userService.AlreadyHasEmail(request.Email);
		
		if(user.Email != request.Email && !emailAlreadyExists)
		{
			var token = await _userService.GenerateChangeEmailTokenAsync(user, request.Email);
			await _userService.ChangeEmailAsync(user, request.Email, token);
		}
	}
	
	private async Task UpdatePassword(UserIdentity user, UpdateUserCommand request)
	{
		var token = await _userService.GeneratePasswordResetTokenAsync(user);
		await _userService.ResetPasswordAsync(user, token, request.Password);
	}
	
	private static void UpdateFieldsWithoutValidation(UserIdentity user, UpdateUserCommand request)
	{
		if(user.Name != request.Name)
		{
			user.Name = request.Name;
		}
		
		if(user.MotherName != request.MotherName)
		{
			user.MotherName = request.MotherName;
		}
		
		if(user.BirthDate != request.BirthDate)
		{
			user.BirthDate = request.BirthDate;
		}
		
		if(user.MobilePhone != request.MobilePhone)
		{
			user.MobilePhone = request.MobilePhone;
		}
		
		if(user.Status != request.Status)
		{
			user.Status = request.Status;
		}
	}
}
