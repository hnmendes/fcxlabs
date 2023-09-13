using FcxLabsUserManagement.Core.Enums;

namespace FcxLabsUserManagement.Core.Contracts;

public interface IUser : IBaseEntity
{
	public string Name { get; set; }
	public string UserName { get; set; }
	public string MobilePhone { get; set; }
	public Status Status { get; set; }
	public string CPF { get; set; }
	public DateTime BirthDate { get; set; }
	public string MotherName { get; set; }
}
