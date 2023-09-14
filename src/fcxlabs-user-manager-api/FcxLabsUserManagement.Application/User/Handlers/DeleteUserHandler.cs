using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Core.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ObjectResult>
{
	private readonly IUserService _userService;

	public DeleteUserHandler(IUserService userService)
	{
		_userService = userService;
	}
	
	public async Task<ObjectResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		var user = await _userService.GetByIdAsync(request.Id);
		
		if(user is null)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User failed to be deleted." })
			{
				StatusCode = StatusCodes.Status404NotFound
			};
		}
		
		await _userService.DeleteUserAsync(user);
		
		return new ObjectResult(new Response { Status = "Success", Message = "User successfully deleted." })
		{
			StatusCode = StatusCodes.Status200OK
		};
	}
}
