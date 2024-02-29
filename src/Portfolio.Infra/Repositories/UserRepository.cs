using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
	private readonly PortfolioContext _context;

	public UserRepository(PortfolioContext context)
		: base(context)
	{
		_context = context;
	}

	public async Task<User> GetUserByNickName(string nickName)
	{
		var user = await _context.Users.AsNoTracking().Where(x => x.NickName == nickName).ToListAsync();

		if (user is null)
		{
			return null;
		}

		return user.FirstOrDefault();
	}
}
