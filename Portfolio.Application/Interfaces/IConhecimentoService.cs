using Portfolio.Application.Dto;
using System.Threading.Tasks;

namespace Portfolio.Application.Interfaces
{
    public interface IConhecimentoService
    {
        Task<ConhecimentoDto> GetConhecimento();
        Task<bool?> AddDefaultConhecimento(ConhecimentoDto model);
        Task<bool?> UpdateConhecimento(ConhecimentoDto model);
    }
}
