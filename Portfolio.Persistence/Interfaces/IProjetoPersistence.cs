using Portfolio.Domain;
using System.Threading.Tasks;

namespace Portfolio.Persistence.Interfaces
{
    public interface IProjetoPersistence
    {
        Task<Projeto> GetProjetoByIdAsync(int id);
        Task<Projeto[]> GetAllProjetoAsync();

    }
}