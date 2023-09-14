using System.Text.Json.Serialization;

namespace FcxLabsUserManagement.Application.Common.Models;

public class Response
{	
	[JsonPropertyName("status")]
	public string Status { get; set; }
	
	[JsonPropertyName("message")]
	public string Message { get; set; }
	
	[JsonPropertyName("content")]
	public object Content { get; set; }
}
