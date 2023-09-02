using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> DeleteSkill(int id)
    {
      var skill = await _context.Skills
        .AsNoTracking()
        .Where(x => x.Id == id)
        .FirstAsync();

      skill.IsActive = false;
      skill.UpdatedAt = DateTime.Now;

      _context.Entry(skill).State = EntityState.Modified;

      await _context.SaveChangesAsync();

      return true;
    }
  }
}