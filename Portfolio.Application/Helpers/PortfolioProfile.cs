using AutoMapper;
using Portfolio.Application.Dto;
using Portfolio.Domain;

namespace Portfolio.Application.Helpers
{
    public class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {

            CreateMap<Projeto, ProjetoDto>();
            CreateMap<ProjetoDto, Projeto>();

            CreateMap<Conhecimento, ConhecimentoDto>();
            CreateMap<ConhecimentoDto, Conhecimento>();

            CreateMap<Perfil, PerfilDto>();
            CreateMap<PerfilDto, Perfil>();

        }
    }
}
