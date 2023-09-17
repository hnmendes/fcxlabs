using System.Linq.Expressions;
using FcxLabsUserManagement.Core.Filters;

namespace FcxLabsUserManagement.Core.Contracts.Services;

public interface IUserService : IServiceBase<UserIdentity>
{
    public Task<bool> DeleteAllSelectedByIdAsync(IEnumerable<string> ids);
    public Task<bool> DeleteUserAsync(UserIdentity user);
    public Task<IReadOnlyList<UserIdentity>> GetAllUsersAsync();
    public Task<IReadOnlyList<UserIdentity>> GetUsersAsync(Expression<Func<UserIdentity, bool>> predicate = null);
    public Task<IReadOnlyList<UserIdentity>> GetUserFilteredAsync(IUserFilter filters);
    public Task<bool> AlreadyHasUserName(string username);
    public Task<bool> AlreadyHasCPF(string cpf);
    public Task<bool> AlreadyHasEmail(string email);
}
