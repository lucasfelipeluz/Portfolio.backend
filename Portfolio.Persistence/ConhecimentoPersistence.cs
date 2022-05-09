using Microsoft.EntityFrameworkCore;
using Portfolio.Domain;
using Portfolio.Persistence.Context;
using Portfolio.Persistence.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Persistence
{
    public class ConhecimentoPersistence : IConhecimentoPersistence
    {
        private readonly PortfolioContext _context;

        public ConhecimentoPersistence(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<Conhecimento> GetConhecimentoAsync(int id)
        {
            IQueryable<Conhecimento> query = _context.Conhecimento;
            query = query.Where(conhecimento => conhecimento.Id == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
