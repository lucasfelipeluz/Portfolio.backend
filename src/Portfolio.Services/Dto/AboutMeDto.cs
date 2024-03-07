namespace Portfolio.Services.Dto;

public class AboutMeDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Text { get; set; }
	public string TextEnglish { get; set; }
	public string JobTitle { get; set; }
	public string JobTitleEnglish { get; set; }
	public string TelegramLink { get; set; }
	public string InstagramLink { get; set; }
	public string LinkedinLink { get; set; }
	public string GithubLink { get; set; }
	public string Address { get; set; }
	public bool IsAvailable { get; set; }
}
