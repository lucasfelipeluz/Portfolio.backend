namespace Portfolio.Domain.Entities;

public class Project : Base
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string UrlWebsite { get; set; } = string.Empty;
	public string UrlGithub { get; set; } = string.Empty;
	public int ViewPriority { get; set; }
	public bool? IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public DateTime? StartedAt { get; set; }
	public DateTime? FinishedAt { get; set; }
	public List<Skill> Skills { get; set; }
	public List<Image> Images { get; set; }
}
