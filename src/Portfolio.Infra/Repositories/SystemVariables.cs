using Microsoft.EntityFrameworkCore;
using Portfolio.Core.ExceptionHandles;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories;

public class SystemVariablesRepository : BaseRepository<SystemVariable>, ISystemVariablesRepository
{
	private readonly PortfolioContext _context;

	public SystemVariablesRepository(PortfolioContext context)
		: base(context)
	{
		_context = context;
	}

	public async Task<SystemVariable> GetByName(string name)
	{
		try
		{
			var variable = await _context
				.SystemVariables.AsNoTracking()
				.Where(x => x.Name == name)
				.FirstOrDefaultAsync();

			return variable;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message);
		}
	}
}
