using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Interfaces;

public interface IProjectRepository : IBaseRepository<Project>
{
	Task<List<Project>> GetByIsActiveAsync(bool isActive);
}
