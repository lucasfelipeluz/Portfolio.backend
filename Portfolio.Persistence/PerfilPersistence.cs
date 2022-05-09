using Microsoft.EntityFrameworkCore;
using Portfolio.Domain;
using Portfolio.Persistence.Context;
using Portfolio.Persistence.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Persistence
{
    public class PerfilPersistence : IPerfilPersistence
    {
        private readonly PortfolioContext _context;

        public PerfilPersistence(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<Perfil> GetPerfil(int id)
        {
            IQueryable<Perfil> query = _context.Perfil;
            query = query.Where(perfil => perfil.Id == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
