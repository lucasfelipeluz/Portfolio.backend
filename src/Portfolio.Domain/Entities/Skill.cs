namespace Portfolio.Domain.Entities;

public class Skill : Base
{
	public string Title { get; set; }
	public string TitleEnglish { get; set; }
	public string Description { get; set; }
	public string DescriptionEnglish { get; set; }
	public DateTime Experience { get; set; }
	public string Color { get; set; }
	public string Icon { get; set; }
	public int ViewPriority { get; set; }
	public bool? IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public List<Project> Projects { get; set; }
	public List<Image> Images { get; set; }
}
