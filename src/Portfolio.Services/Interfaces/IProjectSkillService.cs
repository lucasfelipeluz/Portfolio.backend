using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces
{
  public interface IProjectSkillService
  {
    Task<ProjectSkillDto> CreateProjectSkillAsync(ProjectSkillDto entity);
  }
}