using Portfolio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Persistence.Interfaces
{
    public interface IPerfilPersistence
    {
        Task<Perfil> GetPerfilAsync(int id);
        Task<bool?> EditPerfilAsync(int id, Perfil model);

    }
}
