using AutoMapper;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services
{
  public class ProjectService : IProjectService
  {
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Project> _projectRepository;

    public ProjectService(IMapper mapper, IBaseRepository<Project> projectRepository)
    {
      _mapper = mapper;
      _projectRepository = projectRepository;
    }

    public async Task<List<ProjectDto>> GetAllProjectsAsync()
    {
      var projects = await _projectRepository.GetAllAsync();
      return _mapper.Map<List<ProjectDto>>(projects);
    }

    public async Task<ProjectDto> GetProjectByIdAsync(int id)
    {
      var project = await _projectRepository.GetByIdAsync(id);

      return _mapper.Map<ProjectDto>(project);
    }

    public async Task<ProjectDto> CreateProjectAsync(ProjectDto entity)
    {
      var project = _mapper.Map<Project>(entity);

      await _projectRepository.CreateAsync(project);

      return entity;
    }

    public async Task<bool> UpdateProjectAsync(ProjectDto projectDto)
    {
      var projectExists = await _projectRepository.GetByIdAsync(projectDto.Id);

      if (projectExists == null)
        return false;

      var project = _mapper.Map<Project>(projectDto);

      await _projectRepository.UpdateAsync(project);

      return true;
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
      var project = await _projectRepository.GetByIdAsync(id);

      if (project == null)
        return false;

      await _projectRepository.DeleteAsync(id);

      return true;
    }
  }
}