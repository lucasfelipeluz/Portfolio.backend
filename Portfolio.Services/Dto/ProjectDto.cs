namespace Portfolio.Services.Dto
{
  public class ProjectDto
  {
    public int Id { get; set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string UrlWebsite { get; private set; } = string.Empty;
    public string UrlGithub { get; private set; } = string.Empty;
    public int ViewPriority { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }

  }
}