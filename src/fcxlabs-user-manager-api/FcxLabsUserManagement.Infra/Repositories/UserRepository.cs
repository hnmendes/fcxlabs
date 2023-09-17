using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts;
using FcxLabsUserManagement.Core.Contracts.Repositories;
using FcxLabsUserManagement.Core.Filters;
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
        var result = await _db.Set<UserIdentity>().AsNoTracking().AnyAsync(u => u.CPF == cpf);
        return result;
    }

    public async Task<bool> AlreadyHasEmail(string email)
    {
        var result = await _db.Set<UserIdentity>().AsNoTracking().AnyAsync(u => u.Email == email);
        return result;
    }

    public async Task<bool> AlreadyHasUserName(string username)
    {
        var result = await _db.Set<UserIdentity>().AsNoTracking().AnyAsync(u => u.UserName == username);
        return result;
    }

    public async Task<UserIdentity> GetByIdAsync(string id)
    {
        var result = await GetAsync(u => u.Id == id);
        if (result.Count > 0)
            return result[0];
        return null;
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
        catch (DbUpdateException ex)
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

        foreach (var id in ids)
        {
            var user = await GetByIdAsync(id);

            if (user is not null)
            {
                var result = await LogicalDelete(user);
                userResults.Add(result);
            }
        }

        foreach (var res in userResults)
        {
            if (res.Errors.Count > 0)
                userResult.Errors.AddRange(res.Errors);
        }
        return userResult;
    }

    public IQueryable<UserIdentity> FilterUsers(IUserFilter filterOptions)
    {
        IQueryable<UserIdentity> query = _db.Users;

        if (!string.IsNullOrEmpty(filterOptions.NameContains))
        {
            query = query.Where(u => u.Name.Contains(filterOptions.NameContains));
        }

        if (!string.IsNullOrEmpty(filterOptions.CPFContains))
        {
            query = query.Where(u => u.CPF.Contains(filterOptions.CPFContains));
        }

        if (!string.IsNullOrEmpty(filterOptions.LoginContains))
        {
            query = query.Where(u => u.UserName.Contains(filterOptions.LoginContains));
        }

        if (filterOptions.Status.HasValue)
        {
            query = query.Where(u => u.Status == filterOptions.Status.Value);
        }

        if (filterOptions.BirthDateStart.HasValue)
        {
            query = query.Where(u => u.BirthDate >= filterOptions.BirthDateStart.Value);
        }

        if (filterOptions.BirthDateEnd.HasValue)
        {
            query = query.Where(u => u.BirthDate <= filterOptions.BirthDateEnd.Value);
        }

        if (filterOptions.InsertionDateStart.HasValue)
        {
            query = query.Where(u => u.CreatedOn >= filterOptions.InsertionDateStart.Value);
        }

        if (filterOptions.InsertionDateEnd.HasValue)
        {
            query = query.Where(u => u.CreatedOn <= filterOptions.InsertionDateEnd.Value);
        }

        if (filterOptions.ModificationDateStart.HasValue)
        {
            query = query.Where(u => u.ModifiedOn >= filterOptions.ModificationDateStart.Value);
        }

        if (filterOptions.ModificationDateEnd.HasValue)
        {
            query = query.Where(u => u.ModifiedOn <= filterOptions.ModificationDateEnd.Value);
        }

        if (filterOptions.AgeRangeStart.HasValue && filterOptions.AgeRangeEnd.HasValue)
        {
            var today = DateTime.Today;
            int ageStart = filterOptions.AgeRangeStart.Value;
            int ageEnd = filterOptions.AgeRangeEnd.Value;
            DateTime startDate = today.AddYears(-ageEnd);
            DateTime endDate = today.AddYears(-ageStart);
            query = query.Where(u => u.BirthDate >= startDate && u.BirthDate <= endDate);
        }

        return query;
    }
}
