using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories
{
  public class SkillRepository : BaseRepository<Skill>, ISkillRepository
  {
    private readonly PortfolioContext _context;
    public SkillRepository(PortfolioContext context) : base(context)
    {
      _context = context;
    }
  }
}