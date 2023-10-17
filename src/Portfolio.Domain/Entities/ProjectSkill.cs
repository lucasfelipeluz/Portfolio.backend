namespace Portfolio.Domain.Entities
{
  public class ProjectSkill : Base
  {
    public int SkillId { get; set; }
    public int ProjectId { get; set; }
    public Skill Skill { get; set; }
    public Project Project { get; set; }
  }
}
