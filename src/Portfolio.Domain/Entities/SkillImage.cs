namespace Portfolio.Domain.Entities
{
  public class SkillImage : Base
  {
    public int SkillId { get; set; }
    public int ImageId { get; set; }
    public Skill Skill { get; set; }
    public Image Image { get; set; }
  }
}