using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.Extensions.Conversions;
using FcxLabsUserManagement.Application.User.Queries;
using FcxLabsUserManagement.Core.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ObjectResult>
{
    private readonly IUserService _userService;

    public GetAllUsersHandler(IUserService userService)
	{
        _userService = userService;
    }
	
	public async Task<ObjectResult> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
	{
		var users = (await _userService.GetAllUsersAsync()).ToDTO();
		
		if(users.Count < 0)
		{
			return new ObjectResult(new Response { Status = "Success", Message = "There's no user." })
			{
				StatusCode = StatusCodes.Status204NoContent
			};
		}
		
		return new ObjectResult(new Response { Status = "Success", Content = users })
		{
			StatusCode = StatusCodes.Status200OK
		};
	}
}
