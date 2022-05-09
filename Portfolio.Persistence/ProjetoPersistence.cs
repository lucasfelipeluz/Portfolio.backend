using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Portfolio.Domain;
using Portfolio.Persistence.Context;
using Portfolio.Persistence.Interfaces;

namespace Portfolio.Persistence
{
    public class ProjetoPersistence : IProjetoPersistence
    {
        private readonly PortfolioContext _context;

        public ProjetoPersistence(PortfolioContext context)
        {
            _context = context;
        }
        public async Task<Projeto> GetProjetoByIdAsync(int id)
        {
            IQueryable<Projeto> query = _context.Projeto;
            query = query.OrderBy(projeto => projeto.Id)
                .Where(projeto => projeto.Id == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<Projeto[]> GetAllProjetoAsync()
        {
            IQueryable<Projeto> query = _context.Projeto;

            query = query.OrderBy(projeto => projeto.Id);

            return await query.ToArrayAsync();
        }

    }
}
