using Microsoft.EntityFrameworkCore;
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

	public async Task<SystemVariable> GetVariableAsync(string name)
	{
		var variable = await _context.SystemVariables.AsNoTracking().Where(x => x.Name == name).FirstOrDefaultAsync();

		if (variable is null)
			return null;

		return variable;
	}
}
