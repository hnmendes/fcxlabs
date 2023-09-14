using FcxLabsUserManagement.Application.Common.Models.Auth.Signup;
using FcxLabsUserManagement.Application.Common.ViewModels.User;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.Extensions.Conversions;

public static class ViewModelsExtensions
{
	public static RegisterUserCommand ToRegisterUserCommand(this RegisterUser registerUser, string scheme, IUrlHelper url)
	{
		return new RegisterUserCommand
		{
			Name = registerUser.Name,
			BirthDate = registerUser.BirthDate,
			CPF = registerUser.CPF,
			Email = registerUser.Email,
			UserName = registerUser.UserName,
			MobilePhone = registerUser.MobilePhone,
			MotherName = registerUser.MotherName,
			Password = registerUser.Password,
			Scheme = scheme,
			Url = url
		};
	}
	
	public static CreateUserWithRoleCommand ToCreateUserWithRoleCommand(this RegisterUser registerUser, string role)
	{
		return new CreateUserWithRoleCommand
		{
			Name = registerUser.Name,
			BirthDate = registerUser.BirthDate,
			CPF = registerUser.CPF,
			Email = registerUser.Email,
			UserName = registerUser.UserName,
			MobilePhone = registerUser.MobilePhone,
			MotherName = registerUser.MotherName,
			Password = registerUser.Password,
			Role = role
		};
	}
	
	public static UpdateUserCommand ToUpdateUserCommand(this UpdateUser user, string id)
	{
		return new UpdateUserCommand
		{
			Id = id,
			Name = user.Name,
			BirthDate = user.BirthDate,
			CPF = user.CPF,
			Email = user.Email,
			UserName = user.UserName,
			MobilePhone = user.MobilePhone,
			MotherName = user.MotherName,
			Password = user.Password,
			Status = user.Status,
		};
	}
	
	public static IUser ToDTO(this UserIdentity user)
	{
		return new UserDTO
		{
			Id = user.Id,
			BirthDate = user.BirthDate,
			CPF = user.CPF,
			Email = user.Email,
			CreatedOn = user.CreatedOn,
			EntityId = user.EntityId,
			MobilePhone = user.MobilePhone,
			ModifiedOn = user.ModifiedOn,
			MotherName = user.MotherName,
			Name = user.Name,
			Status = user.Status,
			UserName = user.UserName
		};
	}
	
	public static IReadOnlyList<IUser>ToDTO(this IReadOnlyList<UserIdentity> users)
	{
		var usersDTO = new List<IUser>();
		foreach(var user in users)
		{
			usersDTO.Add(user.ToDTO());
		}
		return usersDTO;
	}
}
