using System.Text.Json.Serialization;
using FcxLabsUserManagement.Core.Enums;

namespace FcxLabsUserManagement.Application.Common.ViewModels.User;

public class UserUpdateVM
{	
	[JsonPropertyName("name")]
	public string Name { get; set; }
	
	[JsonPropertyName("username")]
	public string UserName { get; set; }
	
	[JsonPropertyName("mobilePhone")]
	public string MobilePhone { get; set; }
	
	[JsonPropertyName("status")]
	public Status Status { get; set; }
	
	[JsonPropertyName("cpf")]
	public string CPF { get; set; }
	
	[JsonPropertyName("birthDate")]
	public DateTime BirthDate { get; set; }
	
	[JsonPropertyName("motherName")]
	public string MotherName { get; set; }
}
