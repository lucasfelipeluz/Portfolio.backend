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
		if (!isSuccess)
			return false;

		_cachingRepository.Remove(CacheCode.Skill);
		_cachingRepository.Remove(CacheCode.Project);

		return true;
	}

	public async Task<bool> CreateProjectSkillOnDemandAsync(ProjectSkillOnDemandDto projectSkillOnDemandDto)
	{
		bool isSkillOnDemandForOneProject =
			projectSkillOnDemandDto.ProjectsId.Length == 1 && projectSkillOnDemandDto.SkillsId.Length > 0;
		bool isProjectOnDemandForOneSkill =
			projectSkillOnDemandDto.SkillsId.Length == 1 && projectSkillOnDemandDto.ProjectsId.Length > 0;

		if (isSkillOnDemandForOneProject)
		{
			ProjectSkill[] projectSkills = projectSkillOnDemandDto
				.SkillsId.Select(skillId => new ProjectSkill
				{
					ProjectId = projectSkillOnDemandDto.ProjectsId[0],
					SkillId = skillId
				})
				.ToArray();

			// Create the ProjectSkill's
			for (int i = 0; i < projectSkills.Length; i++)
			{
				_projectSkillRepository.Create(projectSkills[i]);
			}

			await _projectSkillRepository.SaveChangesAsync();

			return true;
		}
		else if (isProjectOnDemandForOneSkill)
		{
			ProjectSkill[] projectSkills = projectSkillOnDemandDto
				.ProjectsId.Select(projectId => new ProjectSkill
				{
					ProjectId = projectId,
					SkillId = projectSkillOnDemandDto.SkillsId[0]
				})
				.ToArray();

			for (int i = 0; i < projectSkills.Length; i++)
			{
				_projectSkillRepository.Create(projectSkills[i]);
			}

			await _projectSkillRepository.SaveChangesAsync();

			return true;
		}

		return false;
	}
}
