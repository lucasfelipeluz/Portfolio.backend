using AutoMapper;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services
{
  public class UserService : IUserService
  {
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(IMapper mapper, IUserRepository userRepository)
    {
      _mapper = mapper;
      _userRepository = userRepository;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
      var users = await _userRepository.GetAllAsync();
      return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
      var user = await _userRepository.GetByIdAsync(id);

      return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByNickNameAsync(string nickName)
    {
      var user = await _userRepository.GetUserByNickName(nickName);

      return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateUserAsync(UserDto userDto)
    {
      var user = _mapper.Map<User>(userDto);

      await _userRepository.CreateAsync(user);

      return userDto;
    }

    public async Task<bool> UpdateUserAsync(UserDto userDto)
    {
      var userExists = await _userRepository.GetByIdAsync(userDto.Id);

      if (userExists == null)
        return false;

      var user = _mapper.Map<User>(userDto);

      await _userRepository.UpdateAsync(user);

      return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
      var user = await _userRepository.GetByIdAsync(id);

      if (user == null)
        return false;

      await _userRepository.DeleteAsync(id);

      return true;
    }
  }
}