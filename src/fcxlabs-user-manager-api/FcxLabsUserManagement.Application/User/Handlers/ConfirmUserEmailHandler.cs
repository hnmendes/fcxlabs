using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.Extensions.Conversions;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts.Services;
using FcxLabsUserManagement.Infra;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class ConfirmUserEmailHandler : IRequestHandler<ConfirmUserEmailCommand, ObjectResult>
{
    private readonly IUserService _userService;

    public ConfirmUserEmailHandler(IUserService userService)
	{
        _userService = userService;
    }
	
	public async Task<ObjectResult> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
	{
		var user = await _userService.GetUserByEmailAsync(request.Email);
		
		if(user is not null)
		{
			var result = await _userService.ConfirmEmailAsync(user, request.Token);
	
			if(!result)
			{
				return new ObjectResult(new Response { Status = "Error", Message = "Failed to confirm email." })
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
