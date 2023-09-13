using FcxLabsUserManagement.Application.Common.Models.Auth.Signup;
using FcxLabsUserManagement.Application.User.Commands;
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
}
