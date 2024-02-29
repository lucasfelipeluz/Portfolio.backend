using AutoMapper;
using Portfolio.Core.Enums;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Cache;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services;

public class ProjectSkillService : IProjectSkillService
{
	private readonly IMapper _mapper;
	private readonly IProjectSkillRepository _projectSkillRepository;
	private readonly ICachingRepository _cachingRepository;

	public ProjectSkillService(
		IMapper mapper,
		IProjectSkillRepository projectSkillRepository,
		ICachingRepository cachingRepository
	)
	{
		_mapper = mapper;
		_projectSkillRepository = projectSkillRepository;
		_cachingRepository = cachingRepository;
	}

	public async Task<bool> CreateProjectSkillAsync(ProjectSkillDto entity)
	{
		var projectSkill = _mapper.Map<ProjectSkill>(entity);

		var isSuccess = await _projectSkillRepository.CreateAsync(projectSkill);
		if (!isSuccess) return false;

		_cachingRepository.Remove(CacheCode.Skill);
		_cachingRepository.Remove(CacheCode.Project);

		return true;
	}
}
