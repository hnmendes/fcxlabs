using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts;
using FcxLabsUserManagement.Core.Contracts.Repositories;
using FcxLabsUserManagement.Infra.IdentityModel;
using Microsoft.EntityFrameworkCore;

namespace FcxLabsUserManagement.Infra.Repositories;

public class UserRepository : RepositoryBase<UserIdentity>, IUserRepository
{
	public UserRepository(UserDbContext db) : base(db)
	{
	}

	public async Task<bool> AlreadyHasCPF(string cpf)
	{
		var result = await _db.Set<UserIdentity>().AnyAsync(u => u.CPF == cpf);
		return result;
	}

	public async Task<bool> AlreadyHasEmail(string email)
	{
		var result = await _db.Set<UserIdentity>().AnyAsync(u => u.Email == email);
		return result;
	}

	public async Task<bool> AlreadyHasUserName(string username)
	{
		var result = await _db.Set<UserIdentity>().AnyAsync(u => u.UserName == username);
		return result;
	}

	public async Task<IUserResult> LogicalDelete(UserIdentity user)
	{
		var userResult = new UserResult();
		try
		{
			user.Status = Core.Enums.Status.INACTIVATED;
			_db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			await _db.SaveChangesAsync();
			return userResult;
		}
		catch(DbUpdateException ex)
		{
			var message = $"LogicalDelete: Error db ocurred; Message: {ex.Message}; StackTrace: {ex.StackTrace}";
			userResult.Errors.Add(message);
			return userResult;
		}
	}

	public async Task<IUserResult> LogicalDeleteAllSelectedByIdAsync(IEnumerable<string> ids)
	{
		var userResults = new List<IUserResult>();
		var userResult = new UserResult();
		
		foreach(var id in ids)
		{
			var user = await GetByIdAsync(id);
			
			if(user is not null)
			{
				var result = await LogicalDelete(user);
				userResults.Add(result);
			}
		}
		
		foreach(var res in userResults)
		{
			if(res.Errors.Count > 0)
				userResult.Errors.AddRange(res.Errors);
		}
		return userResult;
	}
}
