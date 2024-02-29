using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Interfaces;

public interface IImageRepository : IBaseRepository<Image>
{
	Task<bool> AddRelationshipWithProject(ProjectImage projectImage);
	Task<bool> AddRelationshipWithSkill(SkillImage skillImage);
}
