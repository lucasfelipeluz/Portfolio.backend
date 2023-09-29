using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces
{
  public interface IAboutMeService
  {
    Task<AboutMeDto> GetAboutMeAsync();
    Task<bool> UpdateAboutMeAsync(AboutMeDto aboutMeDto);
  }
}