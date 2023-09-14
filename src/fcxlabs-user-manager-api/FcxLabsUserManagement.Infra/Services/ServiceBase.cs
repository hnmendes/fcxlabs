using System.Linq.Expressions;
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

    public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
    {
        return _db.GetAsync(predicate, orderBy, includes, disableTracking);
    }

    public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null)
    {
        return _db.GetAsync(predicate);
    }

    public Task<T> GetByIdAsync(string id)
    {
        return _db.GetByIdAsync(id);
    }

    public Task<T> UpdateAsync(T entity)
	{
		return _db.UpdateAsync(entity);
	}

    Task<IReadOnlyList<T>> IServiceBase<T>.GetAllAsync()
    {
        return _db.GetAllAsync();
    }
}
