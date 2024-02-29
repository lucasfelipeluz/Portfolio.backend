using AutoMapper;
using Portfolio.Core.Enums;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Cache;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;
using Portfolio.Services.Utils;

namespace Portfolio.Services;

public class AboutMeService : IAboutMeService
{
	private readonly IMapper _mapper;
	private readonly IAboutMeRepository _aboutMeRepository;
	private readonly ICachingRepository _cachingRepository;

	public AboutMeService(IMapper mapper, IAboutMeRepository aboutMeRepository, ICachingRepository cachingRepository)
	{
		_mapper = mapper;
		_aboutMeRepository = aboutMeRepository;
		_cachingRepository = cachingRepository;
	}

	public async Task<AboutMeDto> GetAboutMeAsync()
	{
		var cache = _cachingRepository.Get<AboutMeDto>(CacheCode.AboutMe);

		if (cache != null)
		{
			return cache;
		}

		var aboutMe = await _aboutMeRepository.GetAboutMeAsync();
		var aboutMeDto = _mapper.Map<AboutMeDto>(aboutMe);

		_cachingRepository.Save(CacheCode.AboutMe, aboutMeDto);

		return aboutMeDto;
	}

	public async Task<bool> UpdateAboutMeAsync(AboutMeDto aboutMeDto)
	{
		var aboutMe = await _aboutMeRepository.GetAboutMeAsync();

		aboutMeDto.Name = string.IsNullOrEmpty(aboutMeDto.Name) ? aboutMe.Name : aboutMeDto.Name;
		aboutMeDto.Text = string.IsNullOrEmpty(aboutMeDto.Text) ? aboutMe.Text : aboutMeDto.Text;
		aboutMeDto.JobTitle = string.IsNullOrEmpty(aboutMeDto.JobTitle) ? aboutMe.JobTitle : aboutMeDto.JobTitle;
		aboutMeDto.GithubLink = string.IsNullOrEmpty(aboutMeDto.GithubLink)
			? aboutMe.GithubLink
			: aboutMeDto.GithubLink;
		aboutMeDto.LinkedinLink = string.IsNullOrEmpty(aboutMeDto.LinkedinLink)
			? aboutMe.LinkedinLink
			: aboutMeDto.LinkedinLink;
		aboutMeDto.InstagramLink = string.IsNullOrEmpty(aboutMeDto.InstagramLink)
			? aboutMe.InstagramLink
			: aboutMeDto.InstagramLink;
		aboutMeDto.TelegramLink = string.IsNullOrEmpty(aboutMeDto.TelegramLink)
			? aboutMe.TelegramLink
			: aboutMeDto.TelegramLink;
		aboutMeDto.IsAvailable = aboutMeDto.IsAvailable;

		var aboutMeMap = _mapper.Map<AboutMe>(aboutMeDto);
		var isSuccess = await _aboutMeRepository.UpdateAboutMeAsync(aboutMeMap);

		if (!isSuccess)
			return false;

		_cachingRepository.Remove(CacheCode.AboutMe);

		return true;
	}
}
