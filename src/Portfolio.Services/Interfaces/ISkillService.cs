using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface ISkillService
{
	Task<List<SkillDto>> Get();
	Task<List<SkillDto>> GetByIsActive(bool isActive);
	Task<SkillDto> GetById(int id);
	Task<SkillDto> Create(SkillDto skillDto);
	Task<SkillDto> Update(SkillDto skillDto);
	Task<SkillDto> Delete(int id);
}
