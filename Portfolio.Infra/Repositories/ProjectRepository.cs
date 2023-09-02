using Microsoft.EntityFrameworkCore;
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

    public async Task<Project> GetProjectById(int Id)
    {
      var project = await _context
        .Projects
        .Include(x => x.Skills)
        .AsNoTracking()
        .Where(x => x.Id == Id)
        .ToListAsync();

      return project.First();
    }
  }
}