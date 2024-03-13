using AutoMapper;
using Portfolio.Core.Enums;
using Portfolio.Core.ExceptionHandles;
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

	public async Task<List<SkillDto>> Get()
	{
		try
		{
			var cache = _cachingRepository.Get<List<SkillDto>>(CacheCode.Skill);

			if (cache is not null)
			{
				return cache;
			}

			var skills = await _skillRepository.GetAllAsync();
			var skillDto = _mapper.Map<List<SkillDto>>(skills);

			_cachingRepository.Save(CacheCode.Skill, skillDto);

			return skillDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<List<SkillDto>> GetByIsActive(bool isActive)
	{
		try
		{
			var cache = _cachingRepository.Get<List<SkillDto>>(CacheCode.Skill);

			if (cache is not null)
			{
				return cache.Where(x => x.IsActive == isActive).ToList();
			}

			var skills = await _skillRepository.GetByIsActive(isActive);
			var skillDto = _mapper.Map<List<SkillDto>>(skills);

			return skillDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<SkillDto> GetById(int id)
	{
		try
		{
			var cache = _cachingRepository.Get<List<SkillDto>>(CacheCode.Skill);

			if (cache is not null)
			{
				var cacheSkill = cache.Find(x => x.Id == id);

				if (cacheSkill is not null)
					return cacheSkill;
			}

			var skill = await _skillRepository.GetByIdAsync(id);
			var skillDto = _mapper.Map<SkillDto>(skill);

			return skillDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<SkillDto> Create(SkillDto skillDto)
	{
		try
		{
			var skill = _mapper.Map<Skill>(skillDto);

			skill.IsActive = true;

			var createdSkill = await _skillRepository.CreateAsync(skill);

			_cachingRepository.Remove(CacheCode.Skill);

			return _mapper.Map<SkillDto>(createdSkill);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<SkillDto> Update(SkillDto skillDto)
	{
		try
		{
			var skillExists = await GetById(skillDto.Id);

			if (skillExists is null)
				throw new NotFoundEntityException("Skill not found");

			var skill = _mapper.Map<Skill>(skillDto);
			skill.CreatedAt = skillExists.CreatedAt;
			skill.UpdatedAt = DateTime.Now;

			var updatedSkill = await _skillRepository.UpdateAsync(skill);

			_cachingRepository.Remove(CacheCode.Skill);

			return _mapper.Map<SkillDto>(updatedSkill);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<SkillDto> Delete(int id)
	{
		try
		{
			var skillDto = await GetById(id);

			if (skillDto is null)
				throw new NotFoundEntityException("Skill not found");

			var skill = _mapper.Map<Skill>(skillDto);

			var deletedSkill = await _skillRepository.DeleteAsync(skill);

			_cachingRepository.Remove(CacheCode.Skill);

			return _mapper.Map<SkillDto>(deletedSkill);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}
}
