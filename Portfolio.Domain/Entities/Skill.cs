namespace Portfolio.Domain.Entities
{
  public class Skill : Base
  {
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime Experience { get; private set; }
    public string Color { get; private set; } = string.Empty;
    public string Icon { get; private set; }
    public int ViewPriority { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public override bool Validate()
    {
      throw new NotImplementedException();
    }
  }
}
