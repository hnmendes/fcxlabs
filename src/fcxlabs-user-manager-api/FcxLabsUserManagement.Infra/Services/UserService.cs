using FcxLabsUserManagement.Core.Contracts;
using FcxLabsUserManagement.Core.Contracts.Repositories;
using FcxLabsUserManagement.Core.Contracts.Services;

namespace FcxLabsUserManagement.Infra.Services;

public class UserService : ServiceBase<IUser>, IUserService
{
	private readonly IUserRepository _db;
	public UserService(IUserRepository db) : base(db)
	{
		_db = db;
	}

	public Task<IEnumerable<IUser>> DeleteAllSelectedByIdAsync(IEnumerable<string> ids)
	{
		return _db.DeleteAllSelectedByIdAsync(ids);
	}
}
