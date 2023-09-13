using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Infra;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class CreateUserWithRoleHandler : IRequestHandler<CreateUserWithRoleCommand, ObjectResult>
{
	private readonly UserManager<UserIdentity> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	
	public CreateUserWithRoleHandler(UserManager<UserIdentity> userManager, RoleManager<IdentityRole> roleManager)
	{
		_userManager = userManager;
		_roleManager = roleManager;
	}
	
	public async Task<ObjectResult> Handle(CreateUserWithRoleCommand request, CancellationToken cancellationToken)
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
			UserName = request.UserName,
			MotherName = request.MotherName,
			Name = request.Name,
			MobilePhone = request.MobilePhone,
			SecurityStamp = Guid.NewGuid().ToString(),
			Status = Core.Enums.Status.ACTIVATED
		};
		
		if(!await _roleManager.RoleExistsAsync(request.Role))
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User Failed To Create." })
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};
		}
		
		var result = await _userManager.CreateAsync(user, request.Password);
			
		if(!result.Succeeded)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User Failed To Create." })
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};	
		}
		
		await _userManager.AddToRoleAsync(user, request.Role);
		
		return new ObjectResult(new Response { Status = "Success", Message = "User Created Successfully!" })
        {
        	StatusCode = StatusCodes.Status201Created
        };
	}
}
