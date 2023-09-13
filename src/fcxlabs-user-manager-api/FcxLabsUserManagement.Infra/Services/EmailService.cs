using FcxLabsUserManagement.Core.Contracts;
using FcxLabsUserManagement.Core.Contracts.Services;
using FcxLabsUserManagement.Infra.Configurations;
using MailKit.Net.Smtp;
using MimeKit;

namespace FcxLabsUserManagement.Infra.Services;

public class EmailService : IEmailService
{
	private readonly EmailConfig _emailConfig;

	public EmailService(EmailConfig emailConfig)
	{
		_emailConfig = emailConfig;
	}
	
	public async Task SendEmailAsync(IMessage message)
	{
		var emailMessage = CreateEmailMessage(message);
		await SendAsync(emailMessage);
	}
	
	private MimeMessage CreateEmailMessage(IMessage message)
	{
		var emailMessage = new MimeMessage();
		
		emailMessage.From.Add(new MailboxAddress("FcxLabs User Management", _emailConfig.Email));
		emailMessage.To.AddRange(message.ToRecipients.Select(x => new MailboxAddress("FcxLabs User", x)));
		emailMessage.Subject = message.Subject;
		emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
		
		return emailMessage;
	}
	
	private async Task SendAsync(MimeMessage message)
	{		
		using var client = new SmtpClient();
		client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
		client.AuthenticationMechanisms.Remove("XOAUTH2");
		await client.AuthenticateAsync(_emailConfig.Email, _emailConfig.Password);
		await client.SendAsync(message);
	}
}
