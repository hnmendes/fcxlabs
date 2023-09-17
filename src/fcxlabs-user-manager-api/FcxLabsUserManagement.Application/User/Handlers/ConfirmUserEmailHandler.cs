using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class ConfirmUserEmailHandler : IRequestHandler<ConfirmUserEmailCommand, ObjectResult>
{
    private readonly UserManager<UserIdentity> _userManager;

    public ConfirmUserEmailHandler(UserManager<UserIdentity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ObjectResult> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is not null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, request.Token);

            if (!result.Succeeded)
            {
                return new ObjectResult(new Response { Status = "Error", Message = "Falha em confirmar email." })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        return new ObjectResult(new Response { Status = "Success", Message = "Email verified successfully!" })
        {
            StatusCode = StatusCodes.Status200OK
        };
    }
}
