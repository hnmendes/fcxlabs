using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.Common.ViewModels;
using FcxLabsUserManagement.Application.Common.ViewModels.User;
using FcxLabsUserManagement.Application.Extensions;
using FcxLabsUserManagement.Application.Extensions.Conversions;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ObjectResult>
{
	private readonly IUserService _userService;
	private readonly IValidator<UserUpdateVM> _userUpdateValidator;
    private readonly UserManager<UserIdentity> _userManager;

    public UpdateUserHandler(IUserService userService, IValidator<UserUpdateVM> userUpdateValidator, UserManager<UserIdentity> userManager)
	{
		_userService = userService;
		_userUpdateValidator = userUpdateValidator;
        _userManager = userManager;
    }
	
	public async Task<ObjectResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{	
		var user = await _userManager.FindByIdAsync(request.Id);
		IDictionary<string, string[]> errors = new Dictionary<string, string[]>();
		
		if(user is null)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "Usuário não encontrado." })
			{
				StatusCode = StatusCodes.Status404NotFound
			};
		}
		
		var userToPatch = user.ToUserUpdateVM();
		request.PatchedUserUpdate.ApplyTo(userToPatch);
		
		if(!IsValidCPF(userToPatch, user))
		{			
			errors.Add("cpf", new string[] {"O CPF não é válido."});
			return new ObjectResult(new Error(){ Errors = errors })
			{
				StatusCode = StatusCodes.Status400BadRequest
			};
		}
		
		if(await IsCpfAlreadyInUse(userToPatch, user))
		{
			errors.Add("cpf", new string[] {"O CPF já está em uso."});
			return new ObjectResult(new Error(){ Errors = errors })
			{
				StatusCode = StatusCodes.Status400BadRequest
			};
		}
		
		var validationResult = await _userUpdateValidator.ValidateAsync(userToPatch, cancellationToken);
		
		if(!validationResult.IsValid)
		{
			return new ObjectResult(new Error { Errors = validationResult.ToDictionary() })
			{
				StatusCode = StatusCodes.Status400BadRequest
			};
		}
		
		user = userToPatch.ToUserEntity(user);
		var result = await _userManager.UpdateAsync(user);
		
		if(!result.Succeeded)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "Tivemos um problema técnico, mas já estamos resolvendo." })
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};	
		}
		
		return new ObjectResult(new Response { Status = "Success", Message = "Usuário atualizado com sucesso!", Content = user.ToUserVM() })
		{
			StatusCode = StatusCodes.Status201Created
		};
	}
	
	private static bool IsValidCPF(UserUpdateVM userUpdate, UserIdentity user)
	{
		if(userUpdate.CPF == user.CPF)
			return true;
			
		if(!userUpdate.CPF.IsCPFValid())
			return false;
		
		return true;
	}
	
	private async Task<bool> IsCpfAlreadyInUse(UserUpdateVM userUpdate, UserIdentity user)
	{
		if(userUpdate.CPF == user.CPF)
			return false;
			
		return await _userService.AlreadyHasCPF(userUpdate.CPF);
	}
}
