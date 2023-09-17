using FcxLabsUserManagement.Application.Common.ViewModels.User;
using FcxLabsUserManagement.Application.Extensions;
using FcxLabsUserManagement.Core.Contracts.Services;
using FluentValidation;

namespace FcxLabsUserManagement.Application.Common.Validators;

public class UpdateUserValidator : AbstractValidator<UserUpdateVM>
{
	private readonly IUserService _userService;

	public UpdateUserValidator(IUserService userService)
	{
		_userService = userService;
		
		RuleFor(u => u.CPF).NotEmpty().OverridePropertyName("cpf").WithMessage("O CPF deve ser preenchido.");
		RuleFor(u => u.UserName).NotEmpty().OverridePropertyName("username").WithMessage("O login deve ser preenchido.");		
		RuleFor(u => u.BirthDate).NotNull().OverridePropertyName("birthDate").WithMessage("A data de aniversário deve ser preenchida.");
		RuleFor(u => u.MobilePhone).NotEmpty().OverridePropertyName("mobilePhone").WithMessage("O telefone deve ser preenchido.");
		RuleFor(u => u.MotherName).NotEmpty().OverridePropertyName("motherName").WithMessage("O nome da mãe deve ser preenchido.");
		RuleFor(u => u.Name).NotEmpty().OverridePropertyName("name").WithMessage("O seu nome deve ser preenchido.");
	}
}
