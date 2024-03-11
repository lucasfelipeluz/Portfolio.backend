using AutoMapper;
using Microsoft.VisualBasic;
using Portfolio.Core.Enums;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Cache;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services;

public class ProjectService : IProjectService
{
	private readonly IMapper _mapper;
	private readonly IProjectRepository _projectRepository;
	private readonly ICachingRepository _cachingRepository;

	public ProjectService(IMapper mapper, IProjectRepository projectRepository, ICachingRepository cachingRepository)
	{
		_mapper = mapper;
		_projectRepository = projectRepository;
		_cachingRepository = cachingRepository;
	}

	public async Task<List<ProjectDto>> GetAllProjectsAsync()
	{
		var cache = _cachingRepository.Get<List<ProjectDto>>(CacheCode.Project);

		if (cache is not null)
		{
			return cache;
		}

		var projects = await _projectRepository.GetAllAsync();
		var projectsDto = _mapper.Map<List<ProjectDto>>(projects);

		_cachingRepository.Save(CacheCode.Project, projectsDto);

		return projectsDto;
	}

	public async Task<List<ProjectDto>> GetAllProjectsAsync(bool isActive)
	{
		var cache = _cachingRepository.Get<List<ProjectDto>>(CacheCode.Project);

		if (cache is not null)
		{
			return cache.Where(x => x.IsActive == isActive).ToList();
		}

		var projects = await _projectRepository.GetActivesProjects();
		var projectsDto = _mapper.Map<List<ProjectDto>>(projects);

		_cachingRepository.Save(CacheCode.Project, projectsDto);

		return projectsDto;
	}

	public async Task<ProjectDto> GetProjectByIdAsync(int id)
	{
		var cache = _cachingRepository.Get<List<ProjectDto>>(CacheCode.Project);

		if (cache is not null)
		{
			var cacheProduct = cache.Find(x => x.Id == id);

			if (cacheProduct != null)
			{
				return cacheProduct;
			}
		}

		var project = await _projectRepository.GetProjectById(id);

		return _mapper.Map<ProjectDto>(project);
		;
	}

	public async Task<bool> CreateProjectAsync(ProjectDto entity)
	{
		var project = _mapper.Map<Project>(entity);

		project.IsActive = true;

		var isSuccess = await _projectRepository.CreateAsync(project);
		if (!isSuccess)
			return false;

		_cachingRepository.Remove(CacheCode.Project);

		return true;
	}

	public async Task<ProjectDto> CreateProjectAsync(ProjectDto entity, bool returnEntity)
	{
		var project = _mapper.Map<Project>(entity);

		project.IsActive = true;

		var createdProject = await _projectRepository.CreateAsync(project, returnEntity);

		if (createdProject is null)
			return null;

		var projectDto = _mapper.Map<ProjectDto>(createdProject);

		_cachingRepository.Remove(CacheCode.Project);

		return projectDto;
	}

	public async Task<bool> UpdateProjectAsync(ProjectDto projectDto)
	{
		var projectExists = await _projectRepository.GetByIdAsync(projectDto.Id);

		if (projectExists == null)
			return false;

		var project = _mapper.Map<Project>(projectDto);
		project.CreatedAt = projectExists.CreatedAt;
		project.UpdatedAt = DateTime.Now;

		var isSuccess = await _projectRepository.UpdateAsync(project);
		if (!isSuccess)
			return false;

		_cachingRepository.Remove(CacheCode.Project);

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

		_cachingRepository.Remove(CacheCode.Project);

		return true;
	}
}
