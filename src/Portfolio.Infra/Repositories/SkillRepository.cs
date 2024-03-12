using Microsoft.EntityFrameworkCore;
using Portfolio.Core.ExceptionHandles;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories;

public class SkillRepository : BaseRepository<Skill>, ISkillRepository
{
	private readonly PortfolioContext _context;

	public SkillRepository(PortfolioContext context)
		: base(context)
	{
		_context = context;
	}

	public override async Task<List<Skill>> GetAllAsync()
	{
		try
		{
			var skills = await _context
				.Skills.AsNoTracking()
				.OrderByDescending(x => x.ViewPriority)
				.Include(x => x.Projects)
				.ToListAsync();

			return skills;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public async Task<List<Skill>> GetByIsActive(bool isActive)
	{
		try
		{
			var skills = await _context
				.Skills.AsNoTracking()
				.Where(x => x.IsActive == isActive)
				.OrderByDescending(x => x.ViewPriority)
				.Include(x => x.Projects)
				.ToListAsync();

			return skills;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public override async Task<Skill> DeleteAsync(Skill entity)
	{
		var skill = await _context.Skills.AsNoTracking().Where(x => x.Id == entity.Id).FirstAsync();

		skill.IsActive = false;
		skill.UpdatedAt = DateTime.Now;

		_context.Entry(skill).State = EntityState.Modified;

		await _context.SaveChangesAsync();

		return entity;
	}
}
