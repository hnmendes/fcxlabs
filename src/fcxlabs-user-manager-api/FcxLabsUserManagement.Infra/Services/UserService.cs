using System.Linq.Expressions;
using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts.Repositories;
using FcxLabsUserManagement.Core.Contracts.Services;
using FcxLabsUserManagement.Core.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FcxLabsUserManagement.Infra.Services;

public class UserService : ServiceBase<UserIdentity>, IUserService
{
    private readonly IUserRepository _db;
    private readonly ILogger<UserService> _userServiceLogger;

    public UserService(IUserRepository db, ILogger<UserService> userServiceLogger) : base(db)
    {
        _db = db;
        _userServiceLogger = userServiceLogger;
    }

    public Task<IReadOnlyList<UserIdentity>> GetAllUsersAsync()
    {
        return _db.GetAllAsync();
    }

    public Task<IReadOnlyList<UserIdentity>> GetUsersAsync(Expression<Func<UserIdentity, bool>> predicate = null)
    {
        return _db.GetAsync(predicate);
    }

    public async Task<bool> DeleteAllSelectedByIdAsync(IEnumerable<string> ids)
    {
        var result = await _db.LogicalDeleteAllSelectedByIdAsync(ids);
        if (result.Succeeded)
        {
            _userServiceLogger.LogDebug("UpdateUserAsync: User updated sucessfully; ResultValue: {result}", result);
            return result.Succeeded;
        }

        foreach (var errorMessage in result.Errors)
        {
            var index = 1;
            _userServiceLogger.LogError("UpdateUserAsync: User updated failed;Message {index}: {errorMessage}", index, errorMessage);
        }
        return false;
    }

    public async Task<bool> DeleteUserAsync(UserIdentity user)
    {
        var result = await _db.LogicalDelete(user);
        if (result.Succeeded)
        {
            _userServiceLogger.LogDebug("UpdateUserAsync: User updated sucessfully; ResultValue: {result}", result);
            return result.Succeeded;
        }

        foreach (var errorMessage in result.Errors)
        {
            var index = 1;
            _userServiceLogger.LogError("UpdateUserAsync: User updated failed;Message {index}: {errorMessage}", index, errorMessage);
        }
        return false;
    }

    public async Task<UserIdentity> GetUserByEmailAsync(string email)
    {
        var result = await _db.GetAsync(e => e.Email == email);
        if (result.Count > 0)
            return result[0];
        return null;
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

    public async Task<IReadOnlyList<UserIdentity>> GetUserFilteredAsync(IUserFilter filters)
    {
        return await _db.FilterUsers(filters).ToListAsync();
    }
}
