using AutoMapper;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services
{
  public class AboutMeService : IAboutMeService
  {
    private readonly IMapper _mapper;
    private readonly IAboutMeRepository _aboutMeRepository;

    public AboutMeService(IMapper mapper, IAboutMeRepository aboutMeRepository)
    {
      _mapper = mapper;
      _aboutMeRepository = aboutMeRepository;
    }

    public async Task<AboutMeDto> GetAboutMeAsync()
    {
      var aboutMe = await _aboutMeRepository.GetAboutMeAsync();
      return _mapper.Map<AboutMeDto>(aboutMe);
    }

    public async Task<bool> UpdateAboutMeAsync(AboutMeDto aboutMeDto)
    {
      var aboutMe = _mapper.Map<AboutMe>(aboutMeDto);
      var result = await _aboutMeRepository.UpdateAboutMeAsync(aboutMe);
      return result;
    }
  }
}