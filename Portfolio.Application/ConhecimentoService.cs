using AutoMapper;
using Portfolio.Application.Dto;
using Portfolio.Application.Interfaces;
using Portfolio.Domain;
using Portfolio.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace Portfolio.Application
{
    public class ConhecimentoService : IConhecimentoService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IConhecimentoPersistence _conhecimentoPersistence;
        private readonly IMapper _mapper;

        public ConhecimentoService(IGeralPersistence geralPersistence, IConhecimentoPersistence conhecimentoPersistence, IMapper mapper)
        {
            _geralPersistence = geralPersistence;
            _conhecimentoPersistence = conhecimentoPersistence;
            _mapper = mapper;
        }
        public async Task<ConhecimentoDto> GetConhecimento()
        {
            try
            {
                Conhecimento conhecimento = await _conhecimentoPersistence.GetConhecimentoAsync(1);
                if (conhecimento == null) return null;
                ConhecimentoDto conhecimentoDto = _mapper.Map<ConhecimentoDto>(conhecimento);
                return conhecimentoDto;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<bool?> AddDefaultConhecimento(ConhecimentoDto model)
        {
            try
            {
                Conhecimento conhecimento = _mapper.Map<Conhecimento>(model);
                _geralPersistence.Add(conhecimento);
                if (await _geralPersistence.SaveChangesAsync()) { return true; }
                return null;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }


        public async Task<bool?> UpdateConhecimento(ConhecimentoDto model)
        {
            try
            {
                Conhecimento conhecimento = await _conhecimentoPersistence.GetConhecimentoAsync(1);
                if (conhecimento == null) return null;

                model.Id = conhecimento.Id;
                model.DataAtualizacaoConhecimento = DateTime.Now;

                _mapper.Map(model, conhecimento);

                _geralPersistence.Update(conhecimento);
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
