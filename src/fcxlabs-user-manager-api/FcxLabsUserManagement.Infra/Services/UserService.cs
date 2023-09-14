using System.Linq.Expressions;
using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts.Repositories;
using FcxLabsUserManagement.Core.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FcxLabsUserManagement.Infra.Services;

public class UserService : ServiceBase<UserIdentity>, IUserService
{
	private readonly IUserRepository _db;
	private readonly UserManager<UserIdentity> _userManager;
	private readonly ILogger<UserService> _userServiceLogger;
	private readonly RoleManager<IdentityRole> _roleManager;

	public UserService(IUserRepository db, UserManager<UserIdentity> userManager, ILogger<UserService> userServiceLogger, RoleManager<IdentityRole> roleManager) : base(db)
	{
		_db = db;
		_userManager = userManager;
		_userServiceLogger = userServiceLogger;
		_roleManager = roleManager;
	}

	public Task<IReadOnlyList<UserIdentity>> GetAllUsersAsync()
	{
		return _db.GetAllAsync();
	}

	public Task<IReadOnlyList<UserIdentity>> GetUsersAsync(Expression<Func<UserIdentity, bool>> predicate = null)
	{
		return _db.GetAsync(predicate);
	}

	public async Task<UserIdentity> GetUserByIdAsync(string id)
	{
		var user = await _db.GetByIdAsync(id);
		return user;
	}

	public async Task<bool> DeleteAllSelectedByIdAsync(IEnumerable<string> ids)
	{
		var result = await _db.LogicalDeleteAllSelectedByIdAsync(ids);
		if(result.Succeeded)
		{
			_userServiceLogger.LogDebug("UpdateUserAsync: User updated sucessfully; ResultValue: {result}", result);
			return result.Succeeded;
		}
		
		foreach(var errorMessage in result.Errors)
		{
			var index = 1;
			_userServiceLogger.LogError("UpdateUserAsync: User updated failed;Message {index}: {errorMessage}", index, errorMessage);
		}
		return false;
	}

	public async Task<bool> DeleteUserAsync(UserIdentity user)
	{
		var result = await _db.LogicalDelete(user);
		if(result.Succeeded)
		{
			_userServiceLogger.LogDebug("UpdateUserAsync: User updated sucessfully; ResultValue: {result}", result);
			return result.Succeeded;
		}
		
		foreach(var errorMessage in result.Errors)
		{
			var index = 1;
			_userServiceLogger.LogError("UpdateUserAsync: User updated failed;Message {index}: {errorMessage}", index, errorMessage);
		}
		return false;
	}

	public async Task<bool> UpdateUserAsync(UserIdentity user)
	{
		var result = await _userManager.UpdateAsync(user);

		if(result.Succeeded)
		{
			_userServiceLogger.LogDebug("UpdateUserAsync: User updated sucessfully; ResultValue: {result}", result);
			return result.Succeeded;
		}
		
		foreach(var error in result.Errors)
		{
			var index = 1;
			var message = error.Description;
			var code = error.Code;
			_userServiceLogger.LogError("UpdateUserAsync: User updated failed;Code: {code};Message {index}: {message}", index, message, code);
		}
		return false;
	}

	public async Task<bool> CreateUserAsync(UserIdentity user, string password)
	{	
		var result = await _userManager.CreateAsync(user);

		if(result.Succeeded)
		{
			_userServiceLogger.LogDebug("CreateUserAsync: User created sucessfully; ResultValue: {result}", result);			
			return result.Succeeded;	
		}
		
		foreach(var error in result.Errors)
		{
			var index = 1;
			var message = error.Description;
			var code = error.Code;
			
			_userServiceLogger.LogError("CreateUserAsync: User created failed;Code: {code};Message {index}: {message}", index, message, code);
		}
		
		return false;
	}

	public async Task<UserIdentity> GetUserByEmailAsync(string email)
	{
		var result = await _db.GetAsync(e => e.Email == email);
		if(result.Count > 0)
			return result[0];
		return null;
	}

	public async Task<string> GenerateEmailConfirmationTokenAsync(UserIdentity user)
	{	
		var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
		return token;
	}

	public async Task<string> GenerateChangeEmailTokenAsync(UserIdentity user, string newEmail)
	{
		var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
		return token;
	}

	public async Task<bool> AlreadyHasUserName(string username)
	{
		return await _db.AlreadyHasUserName(username);
	}

	public async Task<bool> AlreadyHasCPF(string cpf)
	{
		return await _db.AlreadyHasCPF(cpf);
	}

	public async Task<bool> AlreadyHasEmail(string email)
	{
		return await _db.AlreadyHasEmail(email);
	}

	public async Task<bool> ConfirmEmailAsync(UserIdentity user, string token)
	{
		var result = await _userManager.ConfirmEmailAsync(user, token);
		return result.Succeeded;
	}

	public async Task<bool> AddToRoleAsync(UserIdentity user, string role)
	{
		var result = await _userManager.AddToRoleAsync(user, role);
		return result.Succeeded;
	}

	public async Task<bool> RoleExistsAsync(string role)
	{
		var result = await _roleManager.RoleExistsAsync(role);
		return result;
	}
	
	public async Task<bool> ChangeEmailAsync(UserIdentity user, string newEmail, string token)
	{
		var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
		return result.Succeeded;
	}

	public async Task<string> GeneratePasswordResetTokenAsync(UserIdentity user)
	{
		return await _userManager.GeneratePasswordResetTokenAsync(user);
	}

	public async Task<bool> ResetPasswordAsync(UserIdentity user, string token, string newPassword)
	{
		var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
		return result.Succeeded;
	}
}
