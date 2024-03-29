namespace Portfolio.Services.Dto;

public class ActivityDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string TitleEnglish { get; set; }
	public string Description { get; set; }
	public string DescriptionEnglish { get; set; }
	public string Icon { get; set; }
	public DateTime CreatedAt { get; set; }
}
