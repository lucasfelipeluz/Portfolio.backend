using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories
{
  public class UserRepository : BaseRepository<User>, IUserRepository
  {
    private readonly PortfolioContext _context;
    public UserRepository(PortfolioContext context) : base(context)
    {
      _context = context;
    }
  }
}