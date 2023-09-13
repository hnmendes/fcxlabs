using FcxLabsUserManagement.Core.Contracts.Repositories;
using FcxLabsUserManagement.Core.Contracts.Services;

namespace FcxLabsUserManagement.Infra.Services;

public class ServiceBase<T> : IServiceBase<T> where T : class
{
	private readonly IRepositoryBase<T> _db;
	
	public ServiceBase(IRepositoryBase<T> db)
	{
		_db = db;
	}
	
	public Task<T> CreateAsync(T entity)
	{
		return _db.CreateAsync(entity);
	}

	public Task<T> DeleteAsync(T entity)
	{
		return _db.DeleteAsync(entity);
	}

	public Task<IEnumerable<T>> GetAllAsync()
	{
		return _db.GetAllAsync();
	}

	public Task<T> GetAsync(T entity)
	{
		return _db.GetAsync(entity);
	}

	public Task<T> UpdateAsync(T entity)
	{
		return _db.UpdateAsync(entity);
	}
}
