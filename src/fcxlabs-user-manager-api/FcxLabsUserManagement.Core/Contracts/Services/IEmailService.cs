namespace FcxLabsUserManagement.Core.Contracts.Services;

public interface IEmailService
{
	public Task SendEmailAsync(IMessage message);
}
