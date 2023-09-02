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
      var aboutMe = await _aboutMeRepository.GetAboutMeAsync();

      aboutMeDto.Name = string.IsNullOrEmpty(aboutMeDto.Name) ? aboutMe.Name : aboutMeDto.Name;
      aboutMeDto.Text = string.IsNullOrEmpty(aboutMeDto.Text) ? aboutMe.Text : aboutMeDto.Text;
      aboutMeDto.JobTitle = string.IsNullOrEmpty(aboutMeDto.JobTitle) ? aboutMe.JobTitle : aboutMeDto.JobTitle;
      aboutMeDto.GithubLink = string.IsNullOrEmpty(aboutMeDto.GithubLink) ? aboutMe.GithubLink : aboutMeDto.GithubLink;
      aboutMeDto.LinkedinLink = string.IsNullOrEmpty(aboutMeDto.LinkedinLink) ? aboutMe.LinkedinLink : aboutMeDto.LinkedinLink;
      aboutMeDto.InstagramLink = string.IsNullOrEmpty(aboutMeDto.InstagramLink) ? aboutMe.InstagramLink : aboutMeDto.InstagramLink;
      aboutMeDto.TelegramLink = string.IsNullOrEmpty(aboutMeDto.TelegramLink) ? aboutMe.TelegramLink : aboutMeDto.TelegramLink;
      aboutMeDto.IsAvailable = aboutMeDto.IsAvailable;

      var aboutMeMap = _mapper.Map<AboutMe>(aboutMeDto);
      var result = await _aboutMeRepository.UpdateAboutMeAsync(aboutMeMap);
      return result;
    }
  }
}