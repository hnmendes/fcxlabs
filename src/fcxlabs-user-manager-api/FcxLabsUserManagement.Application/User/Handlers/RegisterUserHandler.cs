using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.Resources;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts.Services;
using FcxLabsUserManagement.Infra.Configs.Options;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ObjectResult>
{
    private readonly UserManager<UserIdentity> _userManager;
    private readonly IEmailService _emailService;
    private readonly EmailConfigOptions _emailConfig;

    public RegisterUserHandler(UserManager<UserIdentity> userManager,
                            IEmailService emailService,
                            IOptions<EmailConfigOptions> emailConfig)
    {
        _userManager = userManager;
        _emailService = emailService;
        _emailConfig = emailConfig.Value;
    }

    public async Task<ObjectResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        UserIdentity user = new()
        {
            Email = request.Email,
            BirthDate = request.BirthDate,
            CPF = request.CPF,
            MotherName = request.MotherName,
            Name = request.Name,
            MobilePhone = request.MobilePhone,
            SecurityStamp = Guid.NewGuid().ToString(),
            Status = Core.Enums.Status.ACTIVATED,
            UserName = request.UserName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return new ObjectResult(new Response { Status = "Error", Message = "Estamos com alguns problemas, já estamos resolvendo." })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        await _userManager.AddToRoleAsync(user, "User");

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = UrlHelperExtensions.Action(request.Url, "ConfirmEmail", "Auth", new { token = token, email = user.Email }, request.Scheme);
        var message = new Message(new string[] { user.Email }, "Confirmação de Email - FcxLabs", String.Format(EmailTemplates.ConfirmEmailTemplate, confirmationLink, _emailConfig.Email));

        await _emailService.SendEmailAsync(message);

        return new ObjectResult(new Response { Status = "Success", Message = "Cadastro realizado com sucesso! Por favor, para acessar sua conta confirme seu email." })
        {
            StatusCode = StatusCodes.Status201Created
        };
    }
}
