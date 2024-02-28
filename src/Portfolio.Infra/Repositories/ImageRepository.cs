using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories;

public class ImageRepository : BaseRepository<Image>, IImageRepository
{
	private readonly PortfolioContext _context;

	public ImageRepository(PortfolioContext context)
		: base(context)
	{
		_context = context;
	}

	public async Task AddRelationshipWithProject(ProjectImage projectImage)
	{
		_context.ProjectsImages.Add(projectImage);
		await _context.SaveChangesAsync();
	}

	public async Task AddRelationshipWithSkill(SkillImage skillImage)
	{
		_context.SkillsImages.Add(skillImage);
		await _context.SaveChangesAsync();
	}
}
