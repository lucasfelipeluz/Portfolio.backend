namespace Portfolio.Services.Dto;

public class ProjectDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string UrlWebsite { get; set; }
	public string UrlGithub { get; set; }
	public int ViewPriority { get; set; }
	public bool IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public DateTime? StartedAt { get; set; }
	public DateTime? FinishedAt { get; set; }
	public List<SkillWithoutIncludeDto> Skills { get; set; }
	public List<ImageWithoutIncludeDto> Images { get; set; }
}
