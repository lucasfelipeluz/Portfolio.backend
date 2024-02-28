namespace Portfolio.Domain.Entities;

public class Image : Base
{
	public string Name { get; set; }
	public string Folder { get; set; }
	public bool? IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public List<Project> Projects { get; set; }
	public List<Skill> Skills { get; set; }
}
