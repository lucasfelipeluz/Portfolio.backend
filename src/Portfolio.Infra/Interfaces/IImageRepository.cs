using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Interfaces
{
  public interface IImageRepository : IBaseRepository<Image>
  {
    Task AddRelationshipWithProject(ProjectImage projectImage);
    Task AddRelationshipWithSkill(SkillImage skillImage);
  }
}