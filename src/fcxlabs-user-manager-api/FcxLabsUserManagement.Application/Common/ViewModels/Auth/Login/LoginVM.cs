using System.Text.Json.Serialization;

namespace FcxLabsUserManagement.Application.Common.ViewModels.Auth.Login;

public class LoginVM
{
	[JsonPropertyName("username")]
	public string Username { get; set; }
	
	[JsonPropertyName("password")]
	public string Password { get; set; }
}
