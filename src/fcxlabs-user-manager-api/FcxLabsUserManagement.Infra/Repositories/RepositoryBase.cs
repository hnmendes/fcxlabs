using FcxLabsUserManagement.Core.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FcxLabsUserManagement.Infra.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
	protected readonly UserDbContext _db;
	
	public RepositoryBase(UserDbContext db)
	{
		_db = db ?? throw new ArgumentNullException(nameof(db));
	}
	
	public async Task<T> CreateAsync(T entity)
	{
		await _db.Set<T>().AddAsync(entity);
		await _db.SaveChangesAsync();
		return entity;
	}

	public async Task<T> DeleteAsync(T entity)
	{
		_db.Set<T>().Remove(entity);
		await _db.SaveChangesAsync();
		return entity;
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		var entities = await _db.Set<T>().AsNoTracking().ToListAsync();
		return entities;
	}

	public async Task<T> GetAsync(T entity)
	{
		return await _db.Set<T>().FindAsync(entity);
	}

	public async Task<T> UpdateAsync(T entity)
	{
		_db.Set<T>().Update(entity);
		await _db.SaveChangesAsync();
		return entity;
	}
}
