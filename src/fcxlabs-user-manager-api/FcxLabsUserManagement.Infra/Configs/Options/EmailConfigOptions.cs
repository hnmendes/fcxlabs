namespace FcxLabsUserManagement.Infra.Configs.Options;

public sealed class EmailConfigOptions
{
	public string Email { get; set; }
	public string SmtpServer { get; set; }
	public int Port { get; set; }
	public string Password { get; set; }
}
