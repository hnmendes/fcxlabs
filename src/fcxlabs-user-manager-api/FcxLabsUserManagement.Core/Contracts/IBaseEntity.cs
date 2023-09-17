namespace FcxLabsUserManagement.Core.Contracts;

public interface IBaseEntity
{
    public DateTime CreatedOn { get; set; }
	public DateTime ModifiedOn { get; set; }
}
