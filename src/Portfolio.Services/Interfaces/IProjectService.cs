using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface IProjectService
{
	Task<List<ProjectDto>> Get();
	Task<List<ProjectDto>> GetByIsActive(bool isActive);
	Task<ProjectDto> GetById(int id);
	Task<ProjectDto> Create(ProjectDto projectDto);
	Task<ProjectDto> Update(ProjectDto projectDto);
	Task<ProjectDto> Delete(int id);
}
