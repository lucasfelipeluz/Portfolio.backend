using AutoMapper;
using Portfolio.Core.Enums;
using Portfolio.Core.ExceptionHandles;
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

	public async Task<List<ImageDto>> Get()
	{
		try
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
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<ImageDto> GetById(int id)
	{
		try
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
			var imageDto = _mapper.Map<ImageDto>(image);

			_cachingRepository.Save(CacheCode.Image, imageDto);

			return imageDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<ImageDto> Create(CreateImageDto imageDto)
	{
		try
		{
			var image = new Image { Name = imageDto.Name, Folder = imageDto.Folder, };

			var newImage = await _imageRepository.CreateAsync(image);

			if (imageDto.ProjectId is not null && imageDto.ProjectId != 0)
			{
				var projectImage = new ProjectImage { ProjectId = imageDto.ProjectId.Value, ImageId = newImage.Id, };

				await _imageRepository.AddRelationshipWithProject(projectImage);
			}
			else if (imageDto.SkillId is not null && imageDto.SkillId != 0)
			{
				var skillImage = new SkillImage { SkillId = imageDto.SkillId.Value, ImageId = newImage.Id, };

				await _imageRepository.AddRelationshipWithSkill(skillImage);
			}

			_cachingRepository.Remove(CacheCode.Image);

			return _mapper.Map<ImageDto>(newImage);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<ImageDto> Delete(int id)
	{
		try
		{
			var imageDto = await GetById(id);

			if (imageDto is null)
				throw new NotFoundEntityException("Image not found");

			var image = _mapper.Map<Image>(imageDto);

			var deletedImage = await _imageRepository.DeleteAsync(image);

			_cachingRepository.Remove(CacheCode.Image);

			return _mapper.Map<ImageDto>(deletedImage);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}
}
