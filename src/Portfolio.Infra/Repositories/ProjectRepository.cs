using Microsoft.EntityFrameworkCore;
using Portfolio.Core.ExceptionHandles;
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

	public override async Task<List<Project>> GetAllAsync()
	{
		try
		{
			var projects = await _context
				.Projects.AsNoTracking()
				.OrderByDescending(x => x.ViewPriority)
				.Include(x => x.Skills)
				.Include(x => x.Images)
				.ToListAsync();

			return projects;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public async Task<List<Project>> GetByIsActive(bool isActive)
	{
		try
		{
			var projects = await _context
				.Projects.AsNoTracking()
				.Where(x => x.IsActive == isActive)
				.OrderByDescending(x => x.ViewPriority)
				.Include(x => x.Skills)
				.Include(x => x.Images)
				.ToListAsync();

			return projects;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public override async Task<Project> GetByIdAsync(int Id)
	{
		try
		{
			var project = await _context
				.Projects.Include(x => x.Skills)
				.AsNoTracking()
				.Where(x => x.Id == Id)
				.ToListAsync();

			return project.First();
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public override async Task<Project> DeleteAsync(Project entity)
	{
		try
		{
			var project = await _context.Projects.AsNoTracking().Where(x => x.Id == entity.Id).FirstAsync();

			project.IsActive = false;
			project.UpdatedAt = DateTime.Now;

			_context.Update(project);

			await _context.SaveChangesAsync();

			return entity;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}
}
