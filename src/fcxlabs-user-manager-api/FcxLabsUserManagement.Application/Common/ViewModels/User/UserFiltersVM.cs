using FcxLabsUserManagement.Core.Enums;
using FcxLabsUserManagement.Core.Filters;

namespace FcxLabsUserManagement.Application.Common.ViewModels.User;

public class UserFiltersVM : IUserFilter
{
    public string NameContains { get; init; }

    public string CPFContains { get; init; }

    public string LoginContains { get; init; }

    public Status? Status { get; init; }

    public DateTime? BirthDateStart { get; init; }

    public DateTime? BirthDateEnd { get; init; }

    public DateTime? InsertionDateStart { get; init; }

    public DateTime? InsertionDateEnd { get; init; }

    public DateTime? ModificationDateStart { get; init; }

    public DateTime? ModificationDateEnd { get; init; }

    public int? AgeRangeStart { get; init; }

    public int? AgeRangeEnd { get; init; }
}
