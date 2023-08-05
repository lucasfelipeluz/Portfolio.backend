using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories
{
  public class ProjectRepository : BaseRepository<Project>, IProjectRepository
  {
    private readonly PortfolioContext _context;
    public ProjectRepository(PortfolioContext context) : base(context)
    {
      _context = context;
    }
  }
}