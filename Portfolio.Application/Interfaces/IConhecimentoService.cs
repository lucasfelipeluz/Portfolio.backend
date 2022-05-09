using Portfolio.Domain;
using System.Threading.Tasks;

namespace Portfolio.Application.Interfaces
{
    internal interface IConhecimentoService
    {
        Task<Conhecimento> GetConhecimento();
        Task<bool?> UpdateConhecimento(Conhecimento model);
    }
}
