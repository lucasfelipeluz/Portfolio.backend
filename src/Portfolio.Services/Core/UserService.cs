using AutoMapper;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Cache;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services
{
  public class UserService : IUserService
  {
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ICachingRepository _cachingRepository;

    public UserService(IMapper mapper, IUserRepository userRepository, ICachingRepository cachingRepository)
    {
      _mapper = mapper;
      _userRepository = userRepository;
      _cachingRepository = cachingRepository;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
      var cache = _cachingRepository.Get<List<UserDto>>(this.ToString());

      if (cache != null)
      {
        return cache;
      }

      var users = await _userRepository.GetAllAsync();
      var userDto = _mapper.Map<List<UserDto>>(users);

      _cachingRepository.Save(this.ToString(), userDto);

      return userDto;
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
      var cache = _cachingRepository.Get<UserDto>(this.ToString());

      if (cache != null)
      {
        return cache;
      }

      var user = await _userRepository.GetByIdAsync(id);
      var userDto = _mapper.Map<UserDto>(user);

      _cachingRepository.Save($"{this.ToString()}/{id}", userDto);

      return userDto;
    }

    public async Task<UserDto> GetUserByNickNameAsync(string nickName)
    {
      var cache = _cachingRepository.Get<UserDto>(this.ToString());

      if (cache != null)
      {
        return cache;
      }

      var user = await _userRepository.GetUserByNickName(nickName);
      var userDto = _mapper.Map<UserDto>(user);

      _cachingRepository.Save($"{this.ToString()}/nickname={nickName}", userDto);

      return userDto;
    }

    public async Task<UserDto> CreateUserAsync(UserDto userDto)
    {
      var user = _mapper.Map<User>(userDto);

      await _userRepository.CreateAsync(user);

      _cachingRepository.Remove(this.ToString());

      return userDto;
    }

    public async Task<bool> UpdateUserAsync(UserDto userDto)
    {
      var userExists = await _userRepository.GetByIdAsync(userDto.Id);

      if (userExists == null)
        return false;

      var user = _mapper.Map<User>(userDto);

      await _userRepository.UpdateAsync(user);

      _cachingRepository.Remove(this.ToString());

      return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
      var user = await _userRepository.GetByIdAsync(id);

      if (user == null)
        return false;

      await _userRepository.DeleteAsync(id);

      _cachingRepository.Remove(this.ToString());

      return true;
    }
  }
}