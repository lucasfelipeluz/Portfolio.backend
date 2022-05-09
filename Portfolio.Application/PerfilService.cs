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
    public class PerfilService: IPerfilService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IPerfilPersistence _perfilPersistence;
        private readonly IMapper _mapper;

        public PerfilService(IGeralPersistence geralPersistence, IPerfilPersistence perfilPersistence, IMapper mapper)
        {
            _geralPersistence = geralPersistence;
            _perfilPersistence = perfilPersistence;
            _mapper = mapper;
        }
        public async Task<PerfilDto> GetPerfil()
        {
            try
            {
                Perfil perfil = await _perfilPersistence.GetPerfil(1);
                if (perfil == null) return null;
                PerfilDto perfilDto = _mapper.Map<PerfilDto>(perfil);
                return perfilDto;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<bool?> AddDefaultPerfil(PerfilDto model)
        {
            try
            {
                
                Perfil perfil = _mapper.Map<Perfil>(model);
                _geralPersistence.Add(perfil);
                if (await _geralPersistence.SaveChangesAsync()) { return true; }
                return null;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<bool?> UpdatePerfil(PerfilDto model)
        {
            try
            {
                Perfil perfil = await _perfilPersistence.GetPerfil(1);
                if (perfil == null) return null;

                model.Id = perfil.Id;
                model.DataAtualizacaoPerfil = DateTime.Now;

                _mapper.Map(model, perfil);

                _geralPersistence.Update(perfil);
                if (await _geralPersistence.SaveChangesAsync()) { return true; }
                return null;

            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}
