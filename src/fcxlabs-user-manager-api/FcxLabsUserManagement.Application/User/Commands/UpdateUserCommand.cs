using FcxLabsUserManagement.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Commands;

public class UpdateUserCommand : IRequest<ObjectResult>
{
	public string Id { get; set; }
	public string Name { get; set; }
	public string Password { get; set; }
	public string UserName { get; set; }
	public string MobilePhone { get; set; }
	public string Email { get; set; }
	public Status Status { get; set; }
	public string CPF { get; set; }
	public DateTime BirthDate { get; set; }
	public string MotherName { get; set; }
}
