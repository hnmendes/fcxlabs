using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.Extensions.Conversions;
using FcxLabsUserManagement.Application.User.Queries;
using FcxLabsUserManagement.Core.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ObjectResult>
{
	private readonly IUserService _userService;

	public GetUserByIdQueryHandler(IUserService userService)
	{
		_userService = userService;
	}
	
	public async Task<ObjectResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		var user = (await _userService.GetByIdAsync(request.Id)).ToDTO();
		
		if(user is null)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "User failed to be fetched." })
			{
				StatusCode = StatusCodes.Status404NotFound
			};
		}
		
		return new ObjectResult(new Response { Status = "Success", Content = user })
		{
			StatusCode = StatusCodes.Status201Created
		};
	}
}
