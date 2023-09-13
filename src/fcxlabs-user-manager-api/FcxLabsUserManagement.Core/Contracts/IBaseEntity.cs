namespace FcxLabsUserManagement.Core.Contracts;

public interface IBaseEntity
{
    public string EntityId { get; }
    public DateTime CreatedOn { get; set; }
	public DateTime ModifiedOn { get; set; }
}
