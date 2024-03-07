using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface ISkillService
{
	Task<List<SkillDto>> GetAllSkillsAsync();
	Task<List<SkillDto>> GetAllSkillsAsync(bool isActive);
	Task<SkillDto> GetSkillByIdAsync(int id);
	Task<bool> CreateSkillAsync(SkillDto skillDto);
	Task<bool> UpdateSkillAsync(SkillDto skillDto);
	Task<bool> DeleteSkillAsync(int id);
}
