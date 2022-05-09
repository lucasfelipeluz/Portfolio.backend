using Portfolio.Domain;
using System.Threading.Tasks;

namespace Portfolio.Application.Interfaces
{
    internal interface IPerfilService
    {
        Task<Perfil> GetPerfil();
        Task<bool?> UpdatePerfil(Perfil model);
    }
}
