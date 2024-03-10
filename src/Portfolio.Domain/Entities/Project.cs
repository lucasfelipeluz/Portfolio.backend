namespace Portfolio.Domain.Entities;

public class Project : Base
{
	public string Title { get; set; }
	public string TitleEnglish { get; set; }
	public string Description { get; set; }
	public string DescriptionEnglish { get; set; }
	public string UrlWebsite { get; set; }
	public string UrlGithub { get; set; }
	public int ViewPriority { get; set; }
	public bool? IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public DateTime? StartedAt { get; set; }
	public DateTime? FinishedAt { get; set; }
	public List<Skill> Skills { get; set; }
	public List<Image> Images { get; set; }
}
