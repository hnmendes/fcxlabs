using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.Extensions.Conversions;
using FcxLabsUserManagement.Application.User.Queries;
using FcxLabsUserManagement.Core.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class GetUsersByFiltersHandler : IRequestHandler<GetUsersByFiltersQuery, ObjectResult>
{
    private readonly IUserService _userService;

    public GetUsersByFiltersHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ObjectResult> Handle(GetUsersByFiltersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userService.GetUserFilteredAsync(request.UserFilters);

        if (users.Count < 0)
        {
            return new ObjectResult(new Response { Status = "Success", Message = "Nenhum usuário encontrado." })
            {
                StatusCode = StatusCodes.Status204NoContent
            };
        }

        return new ObjectResult(new Response { Status = "Success", Content = users.ToUsersVM() })
        {
            StatusCode = StatusCodes.Status200OK
        };
    }
}
