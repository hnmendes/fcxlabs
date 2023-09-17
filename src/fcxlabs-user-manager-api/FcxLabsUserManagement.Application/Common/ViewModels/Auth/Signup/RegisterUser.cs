
using System.Text.Json.Serialization;

namespace FcxLabsUserManagement.Application.Common.Models.Auth.Signup
{
    public class RegisterUser
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		
		[JsonPropertyName("password")]
		public string Password { get; set; }
		
		[JsonPropertyName("username")]
		public string UserName { get; set; }
		
		[JsonPropertyName("mobilePhone")]
		public string MobilePhone { get; set; }
		
		[JsonPropertyName("email")]
		public string Email { get; set; }
		
		[JsonPropertyName("cpf")]
		public string CPF { get; set; }
		
		[JsonPropertyName("birthDate")]
		public DateTime? BirthDate { get; set; }
		
		[JsonPropertyName("motherName")]
		public string MotherName { get; set; }	
	}	
}
