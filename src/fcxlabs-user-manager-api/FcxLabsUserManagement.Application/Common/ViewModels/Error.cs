using System.Text.Json.Serialization;

namespace FcxLabsUserManagement.Application.Common.ViewModels;

public class Error
{
	[JsonPropertyName("errors")]
	public IDictionary<string, string[]> Errors {get; set;}
}
