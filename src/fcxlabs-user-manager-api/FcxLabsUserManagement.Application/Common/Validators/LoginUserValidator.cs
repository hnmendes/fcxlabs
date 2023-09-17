using FcxLabsUserManagement.Application.Common.ViewModels.Auth.Login;
using FluentValidation;

namespace FcxLabsUserManagement.Application.Common.Validators;

public class LoginUserValidator : AbstractValidator<LoginVM>
{
	public LoginUserValidator()
	{
		RuleFor(l => l.Username).NotEmpty().OverridePropertyName("username").WithMessage("O login deve ser preenchido.");
		RuleFor(l => l.Password).NotEmpty().OverridePropertyName("password").WithMessage("A senha deve ser preenchida.");
	}
}
