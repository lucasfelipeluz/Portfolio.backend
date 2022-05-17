using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Identity;
using Portfolio.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Persistence.Interfaces
{
    public class UserPersistence : GeralPersistence, IUserPersistence
    {
        private readonly PortfolioContext _context;

        public UserPersistence(PortfolioContext context): base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsuariosAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUsuarioByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUsuarioByUsernameAsync(string username)
        {
            return await _context.Users
                .SingleOrDefaultAsync(user => user.UserName == username.ToLower());
        }
    }
}
