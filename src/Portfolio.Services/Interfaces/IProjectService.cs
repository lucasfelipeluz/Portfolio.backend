using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface IProjectService
{
	Task<List<ProjectDto>> GetAllProjectsAsync();
	Task<List<ProjectDto>> GetAllProjectsAsync(bool isActive);
	Task<ProjectDto> GetProjectByIdAsync(int id);
	Task<bool> CreateProjectAsync(ProjectDto projectDto);
	Task<bool> UpdateProjectAsync(ProjectDto projectDto);
	Task<bool> DeleteProjectAsync(int id);
}
