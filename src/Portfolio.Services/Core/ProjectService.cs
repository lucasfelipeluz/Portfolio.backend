using AutoMapper;
using Portfolio.Core.Enums;
using Portfolio.Core.ExceptionHandles;
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

	public async Task<List<ProjectDto>> Get()
	{
		try
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
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<List<ProjectDto>> GetByIsActive(bool isActive)
	{
		try
		{
			var cache = _cachingRepository.Get<List<ProjectDto>>(CacheCode.Project);

			if (cache is not null)
			{
				return cache.Where(x => x.IsActive == isActive).ToList();
			}

			var projects = await _projectRepository.GetByIsActiveAsync(isActive);

			var projectsDto = _mapper.Map<List<ProjectDto>>(projects);

			return projectsDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<ProjectDto> GetById(int id)
	{
		try
		{
			var cache = _cachingRepository.Get<List<ProjectDto>>(CacheCode.Project);

			if (cache is not null)
			{
				var cacheProduct = cache.Find(x => x.Id == id);

				if (cacheProduct is not null)
				{
					return cacheProduct;
				}
			}

			var project = await _projectRepository.GetByIdAsync(id);

			return _mapper.Map<ProjectDto>(project);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<ProjectDto> Create(ProjectDto entity)
	{
		try
		{
			var project = _mapper.Map<Project>(entity);

			project.IsActive = true;

			var createdProject = await _projectRepository.CreateAsync(project);

			_cachingRepository.Remove(CacheCode.Project);

			return _mapper.Map<ProjectDto>(createdProject);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<ProjectDto> Update(ProjectDto projectDto)
	{
		try
		{
			var projectExists = await GetById(projectDto.Id);

			if (projectExists is null)
				throw new NotFoundEntityException("Project not found.");

			var project = _mapper.Map<Project>(projectDto);
			project.CreatedAt = projectExists.CreatedAt;
			project.UpdatedAt = DateTime.Now;

			var updatedProject = await _projectRepository.UpdateAsync(project);

			_cachingRepository.Remove(CacheCode.Project);

			return _mapper.Map<ProjectDto>(updatedProject);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<ProjectDto> Delete(int id)
	{
		try
		{
			var projectDto = await GetById(id);

			if (projectDto is null)
				throw new NotFoundEntityException("Project not found.");

			var project = _mapper.Map<Project>(projectDto);

			var deletedProject = await _projectRepository.DeleteAsync(project);

			_cachingRepository.Remove(CacheCode.Project);

			return _mapper.Map<ProjectDto>(deletedProject);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}
}
