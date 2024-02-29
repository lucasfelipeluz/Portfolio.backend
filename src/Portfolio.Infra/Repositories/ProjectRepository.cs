using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
	private readonly PortfolioContext _context;

	public ProjectRepository(PortfolioContext context)
		: base(context)
	{
		_context = context;
	}

	public async Task<List<Project>> GetActivesProjects()
	{
		var projects = await _context
			.Projects.AsNoTracking()
			.Where(x => x.IsActive == true)
			.OrderByDescending(x => x.ViewPriority)
			.Include(x => x.Skills)
			.Include(x => x.Images)
			.ToListAsync();

		return projects;
	}

	public async Task<Project> GetProjectById(int Id)
	{
		var project = await _context
			.Projects.Include(x => x.Skills)
			.AsNoTracking()
			.Where(x => x.Id == Id)
			.ToListAsync();

		if (project.Count == 0)
			return null;

		return project.First();
	}

	public async Task<bool> DeleteProject(int id)
	{
		var project = await _context.Projects.AsNoTracking().Where(x => x.Id == id).FirstAsync();

		if (project is null)
			return false;

		project.IsActive = false;
		project.UpdatedAt = DateTime.Now;

		_context.Entry(project).State = EntityState.Modified;

		await _context.SaveChangesAsync();

		return true;
	}
}
