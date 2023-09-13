using FcxLabsUserManagement.Core.Contracts;
using FcxLabsUserManagement.Core.Contracts.Repositories;

namespace FcxLabsUserManagement.Infra.Repositories;

public class UserRepository : RepositoryBase<IUser>, IUserRepository
{
	public UserRepository(UserDbContext db) : base(db)
	{
		
	}

	public async Task<IEnumerable<IUser>> DeleteAllSelectedByIdAsync(IEnumerable<string> ids)
	{
		var usersToDelete = (await GetAllAsync()).Where(u => ids.Contains(u.EntityId)).ToList();
		_db.RemoveRange(usersToDelete);
		await _db.SaveChangesAsync();
		return usersToDelete;
	}
}
