using Portfolio.Persistence.Context;
using Portfolio.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Persistence
{
    public class GeralPersistence : IGeralPersistence
    {
        private readonly PortfolioContext _context;

        public GeralPersistence (PortfolioContext context)
        {
            _context = context;
        }

        public void Add<T>(T model) where T : class
        {
            _context.Add(model);
        }

        public void Delete<T>(T model) where T : class
        {
            _context.Remove(model);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T model) where T : class
        {
            _context.Update<T>(model);
        }
    }
}
