using Portfolio.Core.ExceptionHandles;
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

	public override async Task<ProjectSkill> CreateAsync(ProjectSkill projectSkill)
	{
		try
		{
			_context.ProjectsSkills.Add(projectSkill);
			await _context.SaveChangesAsync();

			return projectSkill;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message);
		}
	}
}
