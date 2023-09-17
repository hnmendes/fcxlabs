using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.Extensions.Conversions;
using FcxLabsUserManagement.Application.User.Queries;
using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ObjectResult>
{
    private readonly UserManager<UserIdentity> _userManager;

    public GetUserByIdQueryHandler(UserManager<UserIdentity> userManager)
	{
        _userManager = userManager;
    }
	
	public async Task<ObjectResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByIdAsync(request.Id);
		
		if(user is null)
		{
			return new ObjectResult(new Response { Status = "Error", Message = "Usuário não encontrado." })
			{
				StatusCode = StatusCodes.Status404NotFound
			};
		}
		
		var userVM = user.ToUserVM();
		
		return new ObjectResult(new Response { Status = "Success", Content = userVM })
		{
			StatusCode = StatusCodes.Status201Created
		};
	}
}
