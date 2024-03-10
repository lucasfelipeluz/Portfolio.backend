using Portfolio.Core.Enums;

namespace Portfolio.Services.Dto;

public class UserDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string NickName { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public bool? IsActive { get; set; }
	public UserRole Role { get; set; }
	public DateTime? CreatedAt { get; set; }
}
