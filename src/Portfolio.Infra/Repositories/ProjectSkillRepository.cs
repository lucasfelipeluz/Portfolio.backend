using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories;

public class ProjectSkillRepository : BaseRepository<ProjectSkill>, IProjectSkillRepository
{
	private readonly PortfolioContext _context;

	public ProjectSkillRepository(PortfolioContext context)
		: base(context)
	{
		_context = context;
	}

	public async Task<bool> AddRelationship(ProjectSkill projectSkill)
	{
		_context.ProjectsSkills.Add(projectSkill);
		await _context.SaveChangesAsync();

		return true;
	}
}
