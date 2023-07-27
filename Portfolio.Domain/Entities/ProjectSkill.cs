namespace Portfolio.Domain.Entities
{
  public class ProjectSkill : Base
  {
    public int SkillId { get; private set; }
    public int ProjectId { get; private set; }
    public Skill Skill { get; private set; }
    public Project Project { get; private set; }

    public override bool Validate()
    {
      throw new NotImplementedException();
    }
  }
}
