namespace FcxLabsUserManagement.Core.Contracts.Repositories;

public interface IUserRepository : IRepositoryBase<IUser>
{
	Task<IEnumerable<IUser>> DeleteAllSelectedByIdAsync(IEnumerable<string> ids);
}
