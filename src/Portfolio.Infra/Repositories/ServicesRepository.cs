using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;
using Portfolio.Infra.Interfaces;

namespace Portfolio.Infra.Repositories
{
  public class ServicesRepository : BaseRepository<Services>, IServicesRepository
  {
    private readonly PortfolioContext _context;
    public ServicesRepository(PortfolioContext context) : base(context)
    {
      _context = context;
    }

  }
}