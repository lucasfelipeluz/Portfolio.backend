using AutoMapper;
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

	public async Task<ProjectSkillDto> CreateProjectSkillAsync(ProjectSkillDto entity)
	{
		var projectSkill = _mapper.Map<ProjectSkill>(entity);

		await _projectSkillRepository.CreateAsync(projectSkill);

		_cachingRepository.Remove("Projects");
		_cachingRepository.Remove("Skills");

		return entity;
	}
}
