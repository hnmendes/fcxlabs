using FcxLabsUserManagement.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace FcxLabsUserManagement.Infra;

public class UserIdentity : IdentityUser
{
	public string Name { get; set; }
	public string Login { get; set; }
	public string MobilePhone { get; set; }
	public Status Status { get; set; }
	public string CPF { get; set; }
	public DateTime BirthDate { get; set; }
	public string MotherName { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime ModifiedOn { get; set; }
}
