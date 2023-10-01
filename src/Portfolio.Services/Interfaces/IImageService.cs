using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces
{
  public interface IImageService
  {
    Task<List<ImageDto>> GetAllImagesAsync();
    Task<ImageDto> GetImageByIdAsync(int id);
    Task<bool> CreateImageAsync(CreateImageDto imageDto);
    Task<bool> DeleteImageAsync(int id);
  }
}