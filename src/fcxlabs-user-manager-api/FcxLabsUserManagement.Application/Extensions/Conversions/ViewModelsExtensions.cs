using FcxLabsUserManagement.Application.Common.Models.Auth.Signup;
using FcxLabsUserManagement.Application.User.Commands;

namespace FcxLabsUserManagement.Application.Extensions.Conversions;

public static class ViewModelsExtensions
{
	public static RegisterUserCommand ToRegisterUserCommand(this RegisterUser registerUser)
	{
		return new RegisterUserCommand
		{
			Name = registerUser.Name,
			BirthDate = registerUser.BirthDate,
			CPF = registerUser.CPF,
			Email = registerUser.Email,
			Login = registerUser.Login,
			MobilePhone = registerUser.MobilePhone,
			MotherName = registerUser.MotherName,
			Password = registerUser.Password
		};
	}
	
	public static RegisterUserWithRoleCommand ToRegisterUserWithRole(this RegisterUser registerUser, string role)
	{
		return new RegisterUserWithRoleCommand
		{
			Name = registerUser.Name,
			BirthDate = registerUser.BirthDate,
			CPF = registerUser.CPF,
			Email = registerUser.Email,
			Login = registerUser.Login,
			MobilePhone = registerUser.MobilePhone,
			MotherName = registerUser.MotherName,
			Password = registerUser.Password,
			Role = role
		};
	}
}
