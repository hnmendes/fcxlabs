using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Commands;

public class LoginUserCommand : IRequest<ObjectResult>
{
	public string Login { get; set; }
	public string Password { get; set; }
}
