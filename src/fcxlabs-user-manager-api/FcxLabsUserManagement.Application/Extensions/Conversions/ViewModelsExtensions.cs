using FcxLabsUserManagement.Application.Common.Models.Auth.Signup;
using FcxLabsUserManagement.Application.Common.ViewModels.Auth.Login;
using FcxLabsUserManagement.Application.Common.ViewModels.User;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Core;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.Extensions.Conversions;

public static class ViewModelsExtensions
{
	public static RegisterUserCommand ToRegisterUserCommand(this RegisterUser registerUser, string scheme, IUrlHelper url)
	{
		return new RegisterUserCommand
		{
			Name = registerUser.Name,
			BirthDate = registerUser.BirthDate.GetValueOrDefault(),
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
			BirthDate = registerUser.BirthDate.GetValueOrDefault(),
			CPF = registerUser.CPF,
			Email = registerUser.Email,
			UserName = registerUser.UserName,
			MobilePhone = registerUser.MobilePhone,
			MotherName = registerUser.MotherName,
			Password = registerUser.Password,
			Role = role
		};
	}
	
	public static LoginUserCommand ToLoginUserCommand(this LoginVM loginForm)
	{
		return new LoginUserCommand
		{
			Login = loginForm.Username,
			Password = loginForm.Password	
		};
	}
	
	public static UserVM ToUserVM(this UserIdentity user)
	{
		return new UserVM
		{
			Id = user.Id,
			CPF = user.CPF,
			BirthDate = user.BirthDate,
			Email = user.Email,
			MobilePhone = user.MobilePhone,
			MotherName = user.MotherName,
			Name = user.Name,
			Status = user.Status,
			UserName = user.UserName,
			CreatedOn = user.CreatedOn,
			ModifiedOn = user.CreatedOn 
		};
	}
	
	public static UserLoggedVM ToUserLoggedVM(this UserIdentity user, string token)
	{
		return new UserLoggedVM
		{
			Id = user.Id,
			CPF = user.CPF,
			BirthDate = user.BirthDate,
			Email = user.Email,
			MobilePhone = user.MobilePhone,
			MotherName = user.MotherName,
			Name = user.Name,
			Status = user.Status,
			UserName = user.UserName,
			Token = token
		};
	}
	
	public static UserUpdateVM ToUserUpdateVM(this UserIdentity user)
	{
		return new UserUpdateVM 
		{
			UserName = user.UserName,
			BirthDate = user.BirthDate,
			CPF = user.CPF,
			MobilePhone = user.MobilePhone,
			MotherName = user.MotherName,
			Name = user.MotherName,
			Status = user.Status
		};
	}
	
	public static UserIdentity ToUserEntity(this UserUpdateVM userUpdate, UserIdentity user)
	{
		user.UserName = userUpdate.UserName;
		user.BirthDate = userUpdate.BirthDate;
		user.CPF = userUpdate.CPF;
		user.MobilePhone = userUpdate.MobilePhone;
		user.MotherName = userUpdate.MotherName;
		user.Name = userUpdate.Name;
		user.Status = userUpdate.Status;
		
		return user;
	}
	
	
	public static IReadOnlyList<UserVM> ToUsersVM(this IReadOnlyList<UserIdentity> users)
	{
		var usersVM = new List<UserVM>();
		foreach(var user in users)
		{
			usersVM.Add(user.ToUserVM());
		}
		return usersVM;
	}
}
