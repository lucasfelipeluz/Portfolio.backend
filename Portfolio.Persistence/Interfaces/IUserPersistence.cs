using Portfolio.Domain.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Persistence.Interfaces
{
    public interface IUserPersistence : IGeralPersistence
    {
        Task<IEnumerable<User>> GetAllUsuariosAsync();
        Task<User> GetUsuarioByIdAsync(int id);
        Task<User> GetUsuarioByUsernameAsync(string username);

    }
}
