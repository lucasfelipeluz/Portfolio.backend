using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces
{
  public interface IUserService
  {
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> CreateUserAsync(UserDto userDto);
    Task<bool> UpdateUserAsync(UserDto userDto);
    Task<bool> DeleteUserAsync(int id);
  }
}