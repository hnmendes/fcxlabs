namespace FcxLabsUserManagement.Core.Contracts;

public interface IMessage
{
	public List<string> ToRecipients { get; set; }
	public string Subject { get; set; }
	public string Content { get; set; }
}
