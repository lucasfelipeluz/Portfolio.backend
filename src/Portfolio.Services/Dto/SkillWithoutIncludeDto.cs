namespace Portfolio.Services.Dto;

public class SkillWithoutIncludeDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime Experience { get; set; }
	public string Color { get; set; }
	public string Icon { get; set; }
	public int ViewPriority { get; set; }
	public bool IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}
