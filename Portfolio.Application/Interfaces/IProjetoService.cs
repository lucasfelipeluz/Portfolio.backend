using Portfolio.Application.Dto;
using Portfolio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Interfaces
{
    public interface IProjetoService
    {
        Task<ProjetoDto> GetProjeto (int id);
        Task<ProjetoDto[]> GetAllProjetos();
        Task<bool?> AddProjeto (ProjetoDto model);
        Task<bool?> UpdateProjeto (int id, ProjetoDto model);
        Task<bool?> DeleteProjeto (int id);

    }
}
