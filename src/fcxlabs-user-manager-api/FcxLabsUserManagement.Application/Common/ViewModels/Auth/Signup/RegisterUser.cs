using System.ComponentModel.DataAnnotations;

namespace FcxLabsUserManagement.Application.Common.Models.Auth.Signup
{
	public class RegisterUser
	{
		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; }
		
		[Required(ErrorMessage = "Password is required.")]
		public string Password { get; set; }
		
		[Required(ErrorMessage = "Login is required.")]
		public string Login { get; set; }
		
		[Required(ErrorMessage = "MobilePhone is required.")]
		public string MobilePhone { get; set; }
		
		[EmailAddress]
		[Required(ErrorMessage = "Email is required.")]
		public string Email { get; set; }
		
		[Required(ErrorMessage = "CPF is required.")]
		public string CPF { get; set; }
		
		[Required(ErrorMessage = "BirthDate is required.")]
		public DateTime BirthDate { get; set; }
		
		[Required(ErrorMessage = "MotherName is required.")]
		public string MotherName { get; set; }	
	}	
}
