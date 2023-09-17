using FcxLabsUserManagement.Core.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Queries;

public class GetUsersByFiltersQuery : IRequest<ObjectResult>
{
    public IUserFilter UserFilters { get; init; }
}
