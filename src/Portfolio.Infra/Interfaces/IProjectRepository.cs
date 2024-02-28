using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Interfaces;

public interface IProjectRepository : IBaseRepository<Project>
{
	Task<List<Project>> GetActivesProjects();
	Task<Project> GetProjectById(int id);
	Task<bool> DeleteProject(int id);
}
