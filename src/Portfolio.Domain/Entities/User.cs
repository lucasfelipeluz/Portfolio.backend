using Portfolio.Core.Enums;

namespace Portfolio.Domain.Entities;

public class User : Base
{
	public string Name { get; set; }
	public string NickName { get; set; }
	public string Password { get; set; }
	public bool? IsActive { get; set; }
	public UserRole Role { get; set; }
	public DateTime CreatedAt { get; set; }
}
