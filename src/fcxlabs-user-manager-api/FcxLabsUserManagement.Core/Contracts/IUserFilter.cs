using System.Text.Json.Serialization;
using FcxLabsUserManagement.Core.Enums;

namespace FcxLabsUserManagement.Core.Filters;

public interface IUserFilter
{
    [JsonPropertyName("nameContais")]
    public string NameContains { get; init; }

    [JsonPropertyName("cpfContains")]
    public string CPFContains { get; init; }

    [JsonPropertyName("loginContains")]
    public string LoginContains { get; init; }

    [JsonPropertyName("status")]
    public Status? Status { get; init; }

    [JsonPropertyName("birthDateStart")]
    public DateTime? BirthDateStart { get; init; }

    [JsonPropertyName("birthDateEnd")]
    public DateTime? BirthDateEnd { get; init; }

    [JsonPropertyName("insertionDateStart")]
    public DateTime? InsertionDateStart { get; init; }

    [JsonPropertyName("insertionDateEnd")]
    public DateTime? InsertionDateEnd { get; init; }

    [JsonPropertyName("modificationDateStart")]
    public DateTime? ModificationDateStart { get; init; }

    [JsonPropertyName("modificationDateEnd")]
    public DateTime? ModificationDateEnd { get; init; }

    [JsonPropertyName("ageRangeStart")]
    public int? AgeRangeStart { get; init; }

    [JsonPropertyName("ageRangeEnd")]
    public int? AgeRangeEnd { get; init; }
}
