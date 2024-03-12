using AutoMapper;
using Portfolio.Core.Enums;
using Portfolio.Core.ExceptionHandles;
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

	public async Task<AboutMeDto> Get()
	{
		try
		{
			var cache = _cachingRepository.Get<AboutMeDto>(CacheCode.AboutMe);

			if (cache is not null)
			{
				return cache;
			}

			var aboutMe = await _aboutMeRepository.GetAboutMeAsync();
			var aboutMeDto = _mapper.Map<AboutMeDto>(aboutMe);

			_cachingRepository.Save(CacheCode.AboutMe, aboutMeDto);

			return aboutMeDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<AboutMeDto> Update(AboutMeDto aboutMeDto)
	{
		try
		{
			var aboutMe = await Get();

			if (aboutMe is null)
			{
				var postAboutMe = _mapper.Map<AboutMe>(aboutMeDto);

				var newEntity = await _aboutMeRepository.CreateAsync(postAboutMe);

				return _mapper.Map<AboutMeDto>(newEntity);
			}

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
			var updatedEntity = await _aboutMeRepository.UpdateAsync(aboutMeMap);

			_cachingRepository.Remove(CacheCode.AboutMe);

			return _mapper.Map<AboutMeDto>(updatedEntity);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}
}
