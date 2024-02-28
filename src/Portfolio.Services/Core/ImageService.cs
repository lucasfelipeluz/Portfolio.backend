using AutoMapper;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services;

public class ImageService : IImageService
{
	private readonly IMapper _mapper;
	private readonly IImageRepository _imageRepository;

	public ImageService(IMapper mapper, IImageRepository imageRepository)
	{
		_mapper = mapper;
		_imageRepository = imageRepository;
	}

	public async Task<List<ImageDto>> GetAllImagesAsync()
	{
		var images = await _imageRepository.GetAllAsync();
		return _mapper.Map<List<ImageDto>>(images);
	}

	public async Task<ImageDto> GetImageByIdAsync(int id)
	{
		var image = await _imageRepository.GetByIdAsync(id);

		return _mapper.Map<ImageDto>(image);
	}

	public async Task<bool> CreateImageAsync(CreateImageDto imageDto)
	{
		var image = new Image { Name = imageDto.Name, Folder = imageDto.Folder, };

		var newImage = await _imageRepository.CreateAsync(image);

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

		return true;
	}

	public async Task<bool> DeleteImageAsync(int id)
	{
		var image = await _imageRepository.GetByIdAsync(id);

		if (image == null)
			return false;

		await _imageRepository.DeleteAsync(id);

		return true;
	}
}
