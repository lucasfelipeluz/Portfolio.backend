namespace Portfolio.Domain.Entities;

public class AboutMe : Base
{
	public string Name { get; set; } = string.Empty;
	public string Text { get; set; } = string.Empty;
	public string JobTitle { get; set; } = string.Empty;
	public string TelegramLink { get; set; } = string.Empty;
	public string InstagramLink { get; set; } = string.Empty;
	public string LinkedinLink { get; set; } = string.Empty;
	public string GithubLink { get; set; } = string.Empty;
	public bool? IsAvailable { get; set; }
	public DateTime CreatedAt { get; set; }
}
