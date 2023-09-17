using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ObjectResult>
{
	private readonly IUserService _userService;
    private readonly UserManager<UserIdentity> _userManager;

    public DeleteUserHandler(IUserService userService, UserManager<UserIdentity> userManager)
	{
		_userService = userService;
        _userManager = userManager;
    }
	
	public async Task<ObjectResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByIdAsync(request.Id);
		
		if(user is null)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "Usuário não encontrado." })
			{
				StatusCode = StatusCodes.Status404NotFound
			};
		}
		
		await _userService.DeleteUserAsync(user);
		
		return new ObjectResult(new Response { Status = "Success", Message = "Usuário deletado com sucesso." })
		{
			StatusCode = StatusCodes.Status200OK
		};
	}
}
