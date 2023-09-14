using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Queries;

public class GetUserByIdQuery : IRequest<ObjectResult>
{
	public string Id { get; set; }
}
