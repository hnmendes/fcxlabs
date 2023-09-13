using FcxLabsUserManagement.Core.Contracts;

namespace FcxLabsUserManagement.Application.Common.Models;

public class Message : IMessage
{
	public Message(IEnumerable<string> to, string subject, string content)
	{
		ToRecipients = to.ToList();
		Subject = subject;
		Content = content;
	}
	public string Subject { get; set; }
	public string Content { get; set; }
	public List<string> ToRecipients { get; set; }
}
