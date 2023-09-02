using AutoMapper;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services
{
  public class SkillService : ISkillService
  {
    private readonly IMapper _mapper;
    private readonly ISkillRepository _skillRepository;

    public SkillService(IMapper mapper, ISkillRepository skillRepository)
    {
      _mapper = mapper;
      _skillRepository = skillRepository;
    }

    public async Task<List<SkillDto>> GetAllSkillsAsync()
    {
      var skills = await _skillRepository.GetAllAsync();
      return _mapper.Map<List<SkillDto>>(skills);
    }

    public async Task<SkillDto> GetSkillByIdAsync(int id)
    {
      var skill = await _skillRepository.GetByIdAsync(id);

      return _mapper.Map<SkillDto>(skill);
    }

    public async Task<SkillDto> CreateSkillAsync(SkillDto skillDto)
    {
      var skill = _mapper.Map<Skill>(skillDto);

      await _skillRepository.CreateAsync(skill);

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

      return true;
    }

    public async Task<bool> DeleteSkillAsync(int id)
    {
      var project = await _skillRepository.GetByIdAsync(id);

      if (project == null)
        return false;

      await _skillRepository.DeleteAsync(id);

      return true;
    }
  }
}