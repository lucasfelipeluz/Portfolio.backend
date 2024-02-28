namespace Portfolio.Domain.Entities;

public class Skill : Base
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public DateTime Experience { get; set; }
	public string Color { get; set; } = string.Empty;
	public string Icon { get; set; } = string.Empty;
	public int ViewPriority { get; set; }
	public bool? IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public List<Project> Projects { get; set; }
	public List<Image> Images { get; set; }
}
