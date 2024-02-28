using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories;

public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
{
	private readonly PortfolioContext _context;

	public ActivityRepository(PortfolioContext context)
		: base(context)
	{
		_context = context;
	}
}
