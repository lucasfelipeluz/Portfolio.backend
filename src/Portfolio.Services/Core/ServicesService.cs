using AutoMapper;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Interfaces;
using Portfolio.Infra.Cache;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services
{
  public class ServicesService : IServicesService
  {
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly ICachingRepository _cachingRepository;

    public ServicesService(IMapper mapper, IProjectRepository projectRepository, ICachingRepository cachingRepository)
    {
      _mapper = mapper;
      _projectRepository = projectRepository;
      _cachingRepository = cachingRepository;
    }

    public async Task<List<ServicesDto>> GetAllServicesAsync()
    {
      var cache = _cachingRepository.Get<List<ServicesDto>>(this.ToString());

      if (cache != null)
      {
        return cache;
      }

      var projects = await _projectRepository.GetActivesProjects();
      var projectsDto = _mapper.Map<List<ServicesDto>>(projects);

      _cachingRepository.Save(this.ToString(), projectsDto);

      return projectsDto;
    }

    public async Task<ProjectDto> GetProjectByIdAsync(int id)
    {
      var cache = _cachingRepository.Get<ProjectDto>($"{this.ToString()}/{id}");

      if (cache != null)
      {
        return cache;
      }

      var project = await _projectRepository.GetProjectById(id);
      var projectDto = _mapper.Map<ProjectDto>(project);

      _cachingRepository.Save($"{this.ToString()}/{id}", projectDto);

      return projectDto;
    }

    public async Task<ProjectDto> CreateProjectAsync(ProjectDto entity)
    {
      var project = _mapper.Map<Project>(entity);

      await _projectRepository.CreateAsync(project);

      _cachingRepository.Remove(this.ToString());

      return entity;
    }

    public async Task<bool> UpdateProjectAsync(ProjectDto projectDto)
    {
      var projectExists = await _projectRepository.GetByIdAsync(projectDto.Id);

      if (projectExists == null)
        return false;

      var project = _mapper.Map<Project>(projectDto);
      project.CreatedAt = projectExists.CreatedAt;
      project.UpdatedAt = DateTime.Now;

      await _projectRepository.UpdateAsync(project);

      _cachingRepository.Remove(this.ToString());

      return true;
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
      var project = await _projectRepository.GetByIdAsync(id);

      if (project == null)
        return false;

      var response = await _projectRepository.DeleteProject(id);

      if (!response)
        return false;

      _cachingRepository.Remove(this.ToString());

      return true;
    }
  }
}