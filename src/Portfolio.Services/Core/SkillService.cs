using AutoMapper;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Cache;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services;

public class SkillService : ISkillService
{
	private readonly IMapper _mapper;
	private readonly ISkillRepository _skillRepository;
	private readonly ICachingRepository _cachingRepository;

	public SkillService(IMapper mapper, ISkillRepository skillRepository, ICachingRepository cachingRepository)
	{
		_mapper = mapper;
		_skillRepository = skillRepository;
		_cachingRepository = cachingRepository;
	}

	public async Task<List<SkillDto>> GetAllSkillsAsync()
	{
		var cache = _cachingRepository.Get<List<SkillDto>>(this.ToString());

		if (cache != null)
		{
			return cache;
		}

		var skills = await _skillRepository.GetActivesSkills();
		var skillDto = _mapper.Map<List<SkillDto>>(skills);

		_cachingRepository.Save(this.ToString(), skillDto);

		return skillDto;
	}

	public async Task<SkillDto> GetSkillByIdAsync(int id)
	{
		var cache = _cachingRepository.Get<SkillDto>(this.ToString());

		if (cache != null)
		{
			return cache;
		}

		var skill = await _skillRepository.GetByIdAsync(id);
		var skillDto = _mapper.Map<SkillDto>(skill);

		_cachingRepository.Save(this.ToString(), skillDto);

		return skillDto;
	}

	public async Task<SkillDto> CreateSkillAsync(SkillDto skillDto)
	{
		var skill = _mapper.Map<Skill>(skillDto);

		await _skillRepository.CreateAsync(skill);

		_cachingRepository.Remove(this.ToString());

		return skillDto;
	}

	public async Task<bool> UpdateSkillAsync(SkillDto skillDto)
	{
		var skillExists = await _skillRepository.GetByIdAsync(skillDto.Id);

		if (skillExists == null)
			return false;

		var skill = _mapper.Map<Skill>(skillDto);
		skill.CreatedAt = skillExists.CreatedAt;
		skill.UpdatedAt = DateTime.Now;

		await _skillRepository.UpdateAsync(skill);

		_cachingRepository.Remove(this.ToString());

		return true;
	}

	public async Task<bool> DeleteSkillAsync(int id)
	{
		var project = await _skillRepository.GetByIdAsync(id);

		if (project == null)
			return false;

		await _skillRepository.DeleteSkill(id);

		_cachingRepository.Remove(this.ToString());

		return true;
	}
}
