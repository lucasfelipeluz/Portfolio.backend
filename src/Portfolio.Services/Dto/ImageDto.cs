namespace Portfolio.Services.Dto;

public class ImageDto
{
	public string Name { get; set; }
	public string Folder { get; set; }
	public bool IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public List<ProjectWithoutIncludeDto> Projects { get; set; }
	public List<SkillWithoutIncludeDto> Skills { get; set; }
}
