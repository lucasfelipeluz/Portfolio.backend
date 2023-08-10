namespace Portfolio.Services.Dto
{
  public class AboutMeDto
  {
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string Text { get; private set; } = string.Empty;
    public string JobTitle { get; private set; } = string.Empty;
    public string TelegramLink { get; private set; } = string.Empty;
    public string InstagramLink { get; private set; } = string.Empty;
    public string LinkedinLink { get; private set; } = string.Empty;
    public string GithubLink { get; private set; } = string.Empty;
    public bool IsAvailable { get; private set; }
  }
}