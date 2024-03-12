using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Interfaces;

public interface IImageRepository : IBaseRepository<Image>
{
	Task<ProjectImage> AddRelationshipWithProject(ProjectImage projectImage);
	Task<SkillImage> AddRelationshipWithSkill(SkillImage skillImage);
}
