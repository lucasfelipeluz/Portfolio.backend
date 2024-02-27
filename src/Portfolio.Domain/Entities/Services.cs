namespace Portfolio.Domain.Entities
{
  public class Services : Base
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}
