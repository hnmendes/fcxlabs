using FcxLabsUserManagement.Core.Contracts;

namespace FcxLabsUserManagement.Core.Entities;

public class BaseEntity : IBaseEntity
{
    public string EntityId { get; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}
