using FcxLabsUserManagement.Core.Enums;

namespace FcxLabsUserManagement.Application.Common.ViewModels.User;

public class UserVM
{
	public string Id { get; init;}
	public string Name { get; init; }
	public string UserName { get; init; }
	public string MobilePhone { get; init; }
	public Status Status { get; init; }
	public string CPF { get; init; }
	public DateTime BirthDate { get; init; }
	public string MotherName { get; init; }
	public string Email { get; init; }
    public DateTime CreatedOn { get; init; }
    public DateTime ModifiedOn { get; init; }
}
