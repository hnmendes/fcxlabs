using FcxLabsUserManagement.Core.Contracts;
using FcxLabsUserManagement.Core.Enums;

namespace FcxLabsUserManagement.Application.Common.ViewModels.User;

public class UserDTO : IUser
{
	public string Id { get; set; }
	public string Name { get; set; }
	public string UserName { get; set; }
	public string MobilePhone { get; set; }
	public Status Status { get; set; }
	public string CPF { get; set; }
	public DateTime BirthDate { get; set; }
	public string MotherName { get; set; }
	public string Email { get; set; }
	public string EntityId { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime ModifiedOn { get; set; }
}
