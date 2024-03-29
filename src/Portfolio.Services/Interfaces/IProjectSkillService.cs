using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface IProjectSkillService
{
	Task<ProjectSkillDto> Create(ProjectSkillDto entity);
	Task<ProjectSkillDto> CreateOnDemand(ProjectSkillOnDemandDto entity);
}
