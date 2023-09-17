using FcxLabsUserManagement.Application.Common.Models.Auth.Signup;
using FcxLabsUserManagement.Application.Extensions;
using FcxLabsUserManagement.Core.Contracts.Services;
using FluentValidation;

namespace FcxLabsUserManagement.Application.Common.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUser>
{
	private readonly IUserService _userService;

	public RegisterUserValidator(IUserService userService)
	{
		_userService = userService;
		
		//Password
		RuleFor(u => u.Password).Must(p => p.IsPasswordValid()).OverridePropertyName("password").WithMessage("Sua senha precisa ter pelo menos: Um caractere especial, um caractere maiúsculo.");
		RuleFor(u => u.Password).NotEmpty().OverridePropertyName("password").WithMessage("A senha deve ser preenchida.");
		
		//Email
		RuleFor(u => u.Email).NotEmpty().OverridePropertyName("email").WithMessage("O email deve ser preenchido.");
		RuleFor(u => u.Email).EmailAddress().OverridePropertyName("email").WithMessage("O email não é válido.");
		RuleFor(u => u.Email).MustAsync(async (email, cancellation) => 
		{
			bool alreadyHasEmail = await _userService.AlreadyHasEmail(email);
			return !alreadyHasEmail;
		}).OverridePropertyName("email").WithMessage("O email já está em uso.");
		
		//CPF
		RuleFor(u => u.CPF).Must(cpf => cpf.IsCPFValid()).OverridePropertyName("cpf").WithMessage("O CPF não é válido.");
		RuleFor(u => u.CPF).NotEmpty().OverridePropertyName("cpf").WithMessage("O CPF deve ser preenchido.");
		RuleFor(u => u.CPF).MustAsync(async (cpf, cancellation) => 
		{
			bool alreadyHasCPF = await _userService.AlreadyHasCPF(cpf);
			return !alreadyHasCPF;
		}).OverridePropertyName("cpf").WithMessage("Esse CPF já está em uso.");
		
		
		//UserName
		RuleFor(u => u.UserName).NotEmpty().OverridePropertyName("username").WithMessage("O login deve ser preenchido.");
		RuleFor(u => u.UserName).MustAsync(async (userName, cancellation) => 
		{
			bool alreadyHasUserName = await _userService.AlreadyHasUserName(userName);
			return !alreadyHasUserName;
		}).OverridePropertyName("username").WithMessage("O login já está em uso.");
		
		RuleFor(u => u.BirthDate).NotNull().OverridePropertyName("birthDate").WithMessage("A data de aniversário deve ser preenchida.");
		RuleFor(u => u.MobilePhone).NotEmpty().OverridePropertyName("mobilePhone").WithMessage("O telefone deve ser preenchido.");
		RuleFor(u => u.MotherName).NotEmpty().OverridePropertyName("motherName").WithMessage("O nome da mãe deve ser preenchido.");
		RuleFor(u => u.Name).NotEmpty().OverridePropertyName("name").WithMessage("O seu nome deve ser preenchido.");

	}
}
