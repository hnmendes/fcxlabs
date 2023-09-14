using FcxLabsUserManagement.Core.Enums;

namespace FcxLabsUserManagement.Application.Common.ViewModels.User;

public class UpdateUser
{
	public string Name { get; set; }
	public string UserName { get; set; }
	public string MobilePhone { get; set; }
	public string Password { get; set; }
	public Status Status { get; set; }
	public string CPF { get; set; }
	public DateTime BirthDate { get; set; }
	public string MotherName { get; set; }
	public string Email { get; set; }
}
