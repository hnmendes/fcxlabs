using FcxLabsUserManagement.Application.Common.ViewModels.User;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FcxLabsUserManagement.Application.User.Commands;

public class UpdateUserCommand : IRequest<ObjectResult>
{
	public string Id { get; init; }
	public JsonPatchDocument<UserUpdateVM> PatchedUserUpdate { get; init;}
	
}
