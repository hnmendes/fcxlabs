namespace FcxLabsUserManagement.Infra.Configs.Options;

public sealed class JwtOptions
{
	public string ValidAudience { get; set; }
	public string ValidIssuer { get; set; }
	public string Secret { get; set; }
}
