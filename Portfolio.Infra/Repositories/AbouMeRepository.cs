using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories
{
  public class AboutMeRepository : BaseRepository<AboutMe>, IAboutMeRepository
  {
    private readonly PortfolioContext _context;
    public AboutMeRepository(PortfolioContext context) : base(context)
    {
      _context = context;
    }
    public async Task<AboutMe> GetAboutMeAsync()
    {
      var aboutMe = await _context.Set<AboutMe>()
        .AsNoTracking()
        .OrderBy(e => e.CreatedAt)
        .ToListAsync();

      return aboutMe.First();
    }

    public async Task<bool> UpdateAboutMeAsync(AboutMe aboutMe)
    {
      _context.Add(aboutMe);
      await _context.SaveChangesAsync();

      return true;
    }
  }
}