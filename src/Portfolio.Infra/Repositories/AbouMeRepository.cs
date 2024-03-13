using Microsoft.EntityFrameworkCore;
using Portfolio.Core.ExceptionHandles;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories;

public class AboutMeRepository : BaseRepository<AboutMe>, IAboutMeRepository
{
	private readonly PortfolioContext _context;

	public AboutMeRepository(PortfolioContext context)
		: base(context)
	{
		_context = context;
	}

	public async Task<AboutMe> GetAboutMeAsync()
	{
		try
		{
			var aboutMe = await _context.Set<AboutMe>().AsNoTracking().OrderBy(e => e.CreatedAt).ToListAsync();

			if (aboutMe.Count == 0 || aboutMe is null)
			{
				return null;
			}

			return aboutMe.Last();
		}
		catch (Exception ex)
		{
			throw new RepositoryException(ex.Message, ex);
		}
	}
}
