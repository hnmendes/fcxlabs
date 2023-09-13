namespace FcxLabsUserManagement.Core.Contracts.Repositories;

public interface IRepositoryBase<T> where T : class
{
	Task<T> CreateAsync(T entity);
	Task<T> UpdateAsync(T entity);
	Task<T> DeleteAsync(T entity);
	Task<T> GetAsync(T entity);
	Task<IEnumerable<T>> GetAllAsync();
}
