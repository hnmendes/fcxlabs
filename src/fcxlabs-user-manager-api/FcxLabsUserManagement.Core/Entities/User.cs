using FcxLabsUserManagement.Core.Contracts;
using FcxLabsUserManagement.Core.Enums;

namespace FcxLabsUserManagement.Core.Entities;

public class User : IBaseEntity, IUser
{
    public string Name { get; set; }
	public string UserName { get; set; }
	public string MobilePhone { get; set; }
	public Status Status { get; set; }
	public string CPF { get; set; }
	public DateTime BirthDate { get; set; }
	public string MotherName { get; set; }
    public string EntityId { get; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public string Email { get; set; }
    public string Id { get; set; }
}
