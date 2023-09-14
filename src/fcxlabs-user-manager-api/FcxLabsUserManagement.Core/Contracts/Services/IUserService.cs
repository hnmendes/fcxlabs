using System.Linq.Expressions;

namespace FcxLabsUserManagement.Core.Contracts.Services;

public interface IUserService : IServiceBase<UserIdentity>
{
	public Task<bool> DeleteAllSelectedByIdAsync(IEnumerable<string> ids);
	public Task<bool> DeleteUserAsync(UserIdentity user);
	public Task<bool> UpdateUserAsync(UserIdentity user);
	public Task<bool> CreateUserAsync(UserIdentity user, string password);
	public Task<IReadOnlyList<UserIdentity>> GetAllUsersAsync();
	public Task<IReadOnlyList<UserIdentity>> GetUsersAsync(Expression<Func<UserIdentity, bool>> predicate = null);
	public Task<UserIdentity> GetUserByIdAsync(string id);
	public Task<UserIdentity> GetUserByEmailAsync(string email);
	public Task<string> GenerateEmailConfirmationTokenAsync(UserIdentity user);
	public Task<string> GenerateChangeEmailTokenAsync(UserIdentity user, string newEmail);
	public Task<string> GeneratePasswordResetTokenAsync(UserIdentity user);
	public Task<bool> AlreadyHasUserName(string username);
	public Task<bool> AlreadyHasCPF(string cpf);
	public Task<bool> AlreadyHasEmail(string email);
	public Task<bool> ConfirmEmailAsync(UserIdentity user, string token);
	public Task<bool> AddToRoleAsync(UserIdentity user, string role);
	public Task<bool> RoleExistsAsync(string role);
	public Task<bool> ChangeEmailAsync(UserIdentity user, string newEmail, string token);
	public Task<bool> ResetPasswordAsync(UserIdentity user, string token, string newPassword);
}
