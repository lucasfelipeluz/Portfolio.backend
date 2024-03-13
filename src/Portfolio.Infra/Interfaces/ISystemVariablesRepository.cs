using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Interfaces;

public interface ISystemVariablesRepository : IBaseRepository<SystemVariable>
{
	public Task<SystemVariable> GetByName(string name);
}
