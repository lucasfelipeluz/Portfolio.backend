using AutoMapper;
using Portfolio.Core.Enums;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Cache;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services;

public class ImageService : IImageService
{
	private readonly IMapper _mapper;
	private readonly IImageRepository _imageRepository;
	private readonly ICachingRepository _cachingRepository;

	public ImageService(IMapper mapper, IImageRepository imageRepository, ICachingRepository cachingRepository)
	{
		_mapper = mapper;
		_imageRepository = imageRepository;
		_cachingRepository = cachingRepository;
	}

	public async Task<List<ImageDto>> GetAllImagesAsync()
	{
		var cache = _cachingRepository.Get<List<ImageDto>>(CacheCode.Image);
		if (cache is not null)
		{
			return cache;
		}

		var images = await _imageRepository.GetAllAsync();
		var imagesDto = _mapper.Map<List<ImageDto>>(images);

		_cachingRepository.Save(CacheCode.Image, imagesDto);

		return imagesDto;
	}

	public async Task<ImageDto> GetImageByIdAsync(int id)
	{
		var cache = _cachingRepository.Get<List<ImageDto>>(CacheCode.Image);
		if (cache is not null)
		{
			var cacheImage = cache.Find(x => x.Id == id);

			if (cacheImage is not null)
			{
				return cacheImage;
			}
		}

		var image = await _imageRepository.GetByIdAsync(id);

		return _mapper.Map<ImageDto>(image);
	}

	public async Task<bool> CreateImageAsync(CreateImageDto imageDto)
	{
		var image = new Image { Name = imageDto.Name, Folder = imageDto.Folder, };

		var newImage = await _imageRepository.CreateAsync(image, true);

		if (imageDto.ProjectId != null && imageDto.ProjectId != 0)
		{
			var projectImage = new ProjectImage { ProjectId = imageDto.ProjectId.Value, ImageId = newImage.Id, };

			await _imageRepository.AddRelationshipWithProject(projectImage);
		}
		else if (imageDto.SkillId != null && imageDto.SkillId != 0)
		{
			var skillImage = new SkillImage { SkillId = imageDto.SkillId.Value, ImageId = newImage.Id, };

			await _imageRepository.AddRelationshipWithSkill(skillImage);
		}

		_cachingRepository.Remove(CacheCode.Image);

		return true;
	}

	public async Task<bool> DeleteImageAsync(int id)
	{
		var image = await _imageRepository.GetByIdAsync(id);

		if (image == null)
			return false;

		var isSuccess = await _imageRepository.DeleteAsync(id);
		if (!isSuccess)
			return false;

		_cachingRepository.Remove(CacheCode.Image);

		return true;
	}
}
