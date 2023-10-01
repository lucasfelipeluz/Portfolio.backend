using Portfolio.Domain.Entities;

namespace Portfolio.Services.Dto
{
  public class ImageDto
  {
    public string Name { get; set; }
    public string Folder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<Project> Projects { get; set; }
    public List<Skill> Skills { get; set; }
  }
}