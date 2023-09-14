using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Queries;

public class GetAllUsersQuery : IRequest<ObjectResult>
{
}
