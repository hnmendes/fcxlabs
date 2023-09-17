using System.Reflection;
using FcxLabsUserManagement.Application.Common.Models.Auth.Signup;
using FcxLabsUserManagement.Application.Common.Validators;
using FcxLabsUserManagement.Application.Common.ViewModels.Auth.Login;
using FcxLabsUserManagement.Application.Common.ViewModels.User;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FcxLabsUserManagement.Application;

public static class DIExtensionApplication
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		
		//Validators
		services.AddScoped<IValidator<RegisterUser>, RegisterUserValidator>();
		services.AddScoped<IValidator<LoginVM>, LoginUserValidator>();
		services.AddScoped<IValidator<UserUpdateVM>, UpdateUserValidator>();
		
		return services;
	}
}
