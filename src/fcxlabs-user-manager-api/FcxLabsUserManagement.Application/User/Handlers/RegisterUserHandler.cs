using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Infra;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class RegisterUserHandler: IRequestHandler<RegisterUserCommand, ObjectResult>
{	
	private readonly UserManager<UserIdentity> _userManager;
	
	public RegisterUserHandler(UserManager<UserIdentity> userManager)
	{
		_userManager = userManager;
	}

	public async Task<ObjectResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
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
			Login = request.Login,
			MotherName = request.MotherName,
			Name = request.Name,
			MobilePhone = request.MobilePhone,
			SecurityStamp = Guid.NewGuid().ToString(),
			Status = Core.Enums.Status.ACTIVATED
		};
		
		var result = await _userManager.CreateAsync(user, request.Password);
		
		if(!result.Succeeded)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User Failed To Created." })
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};	
		}
		
		return new ObjectResult(new Response { Status = "Success", Message = "User Created Successfully!" })
		{
			StatusCode = StatusCodes.Status201Created
		};
	}
}
