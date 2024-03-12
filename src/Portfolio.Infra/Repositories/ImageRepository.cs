using Microsoft.EntityFrameworkCore;
using Portfolio.Core.ExceptionHandles;
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

	public async Task<ProjectImage> AddRelationshipWithProject(ProjectImage projectImage)
	{
		try
		{
			_context.ProjectsImages.Add(projectImage);
			await _context.SaveChangesAsync();

			return projectImage;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}

	public async Task<SkillImage> AddRelationshipWithSkill(SkillImage skillImage)
	{
		try
		{
			_context.SkillsImages.Add(skillImage);
			await _context.SaveChangesAsync();

			return skillImage;
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}
}
