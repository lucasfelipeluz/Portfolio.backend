using AutoMapper;
using Portfolio.Application.Dto;
using Portfolio.Application.Interfaces;
using Portfolio.Domain;
using Portfolio.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application
{
    public class ProjetoService : IProjetoService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IProjetoPersistence _projetoPersistence;
        private readonly IMapper _mapper;

        public ProjetoService(IGeralPersistence geralPersistence, IProjetoPersistence projetoPersistence, IMapper mapper)
        {
            _geralPersistence = geralPersistence;
            _projetoPersistence = projetoPersistence;
            _mapper = mapper;
        }

        public async Task<ProjetoDto[]> GetAllProjetos()
        {
            try
            {
                Projeto[] projetos = await _projetoPersistence.GetAllProjetoAsync();
                if (projetos == null) return null;
                ProjetoDto[] projetosDto = _mapper.Map<ProjetoDto[]>(projetos);
                return projetosDto;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<ProjetoDto> GetProjeto(int id)
        {
            try
            {
                Projeto projeto = await _projetoPersistence.GetProjetoByIdAsync(id);
                if (projeto == null) return null;
                ProjetoDto projetoDto = _mapper.Map<ProjetoDto>(projeto);
                return projetoDto;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<bool?> AddProjeto(ProjetoDto model)
        {
            try
            {
                model.DataAtualizacaoProjeto = DateTime.Now;

                Projeto projeto = _mapper.Map<Projeto>(model);
                _geralPersistence.Add(projeto);
                if (await _geralPersistence.SaveChangesAsync()) { return true; }
                return null;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<bool?> DeleteProjeto(int id)
        {
            try
            {
                Projeto projeto = await _projetoPersistence.GetProjetoByIdAsync(id);
                if (projeto == null) return null;
                _geralPersistence.Delete(projeto);
                if (await _geralPersistence.SaveChangesAsync()) { return true; }
                return null;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<bool?> UpdateProjeto(int id, ProjetoDto model)
        {
            try
            {
                Projeto projeto = await _projetoPersistence.GetProjetoByIdAsync(id);
                if(projeto == null) return null;

                model.Id = projeto.Id;
                model.DataAtualizacaoProjeto = DateTime.Now;

                _mapper.Map(model, projeto);

                _geralPersistence.Update(projeto);
                if(await _geralPersistence.SaveChangesAsync()) { return true; }
                return null;
                
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}
