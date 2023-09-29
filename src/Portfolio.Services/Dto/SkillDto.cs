namespace Portfolio.Services.Dto
{
  public class SkillDto
  {
    public int Id { get; set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime Experience { get; private set; }
    public string Color { get; private set; } = string.Empty;
    public string Icon { get; private set; } = string.Empty;
    public int ViewPriority { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
  }
}