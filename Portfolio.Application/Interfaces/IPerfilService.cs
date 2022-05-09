using Portfolio.Application.Dto;
using System.Threading.Tasks;

namespace Portfolio.Application.Interfaces
{
    public interface IPerfilService
    {
        Task<PerfilDto> GetPerfil();
        Task<bool?> AddDefaultPerfil(PerfilDto model);
        Task<bool?> UpdatePerfil(PerfilDto model);
    }
}
