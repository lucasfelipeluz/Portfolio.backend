using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface IAboutMeService
{
	Task<AboutMeDto> Get();
	Task<AboutMeDto> Update(AboutMeDto aboutMeDto);
}
