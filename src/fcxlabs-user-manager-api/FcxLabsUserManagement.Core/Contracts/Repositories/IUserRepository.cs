namespace FcxLabsUserManagement.Core.Contracts.Repositories;

public interface IUserRepository : IRepositoryBase<UserIdentity>
{
	public Task<IUserResult> LogicalDeleteAllSelectedByIdAsync(IEnumerable<string> ids);
	public Task<IUserResult> LogicalDelete(UserIdentity user);
	public Task<bool> AlreadyHasUserName(string username);
	public Task<bool> AlreadyHasCPF(string cpf);
	public Task<bool> AlreadyHasEmail(string email);
}
