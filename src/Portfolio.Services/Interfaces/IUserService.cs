using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface IUserService
{
	Task<List<UserDto>> Get();
	Task<UserDto> GetUserById(int id);
	Task<UserDto> GetUserByNickName(string nickName);
	Task<UserDto> Create(UserDto userDto);
	Task<UserDto> Update(UserDto userDto);
	Task<UserDto> Delete(int id);
}
