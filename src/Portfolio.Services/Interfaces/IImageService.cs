using Portfolio.Domain.Entities;
using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface IImageService
{
	Task<List<ImageDto>> Get();
	Task<ImageDto> GetById(int id);
	Task<ImageDto> Create(CreateImageDto imageDto);
	Task<ImageDto> Delete(int id);
}
