using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface IProjectSkillService
{
	Task<bool> CreateProjectSkillAsync(ProjectSkillDto entity);
	Task<bool> CreateProjectSkillOnDemandAsync(ProjectSkillOnDemandDto entity);
}
