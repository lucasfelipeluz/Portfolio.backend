using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Interfaces;

public interface ISkillRepository : IBaseRepository<Skill>
{
	public Task<List<Skill>> GetByIsActive(bool isActive);
}
