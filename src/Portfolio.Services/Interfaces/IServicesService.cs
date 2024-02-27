using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces
{
  public interface IServicesService
  {
    Task<List<ServicesDto>> GetAllServicesAsync();
    Task<ServicesDto> GetServiceByIdAsync(int id);
    Task<ServicesDto> CreateServiceAsync(ServicesDto servicesDto);
    Task<bool> UpdateServiceAsync(ServicesDto servicesDto);
    Task<bool> DeleteServiceAsync(int id);
  }
}