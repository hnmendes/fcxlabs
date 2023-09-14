namespace FcxLabsUserManagement.Core.Contracts;

public interface IUserResult
{
	public List<string> Errors { get; set; }
	public bool Succeeded { get; }
}
