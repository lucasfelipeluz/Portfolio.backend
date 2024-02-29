using AutoMapper;
using Portfolio.Core.Enums;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Cache;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services;

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
		var cache = _cachingRepository.Get<List<UserDto>>(CacheCode.User);

		if (cache is not null)
		{
			return cache;
		}

		var users = await _userRepository.GetAllAsync();
		var userDto = _mapper.Map<List<UserDto>>(users);

		_cachingRepository.Save(CacheCode.User, userDto);

		return userDto;
	}

	public async Task<UserDto> GetUserByIdAsync(int id)
	{
		var cache = _cachingRepository.Get<List<UserDto>>(CacheCode.User);

		if (cache is not null)
		{
			var cacheUser = cache.Find(x => x.Id == id);

			if (cacheUser is not null) return cacheUser;
		}

		var user = await _userRepository.GetByIdAsync(id);
		var userDto = _mapper.Map<UserDto>(user);

		return userDto;
	}

	public async Task<UserDto> GetUserByNickNameAsync(string nickName)
	{
		var cache = _cachingRepository.Get<List<UserDto>>(CacheCode.User);

		if (cache is not null)
		{
			var cacheUser = cache.Find(x => x.NickName == nickName);

			if (cacheUser is not null) return cacheUser;
		}

		var user = await _userRepository.GetUserByNickName(nickName);
		var userDto = _mapper.Map<UserDto>(user);

		return userDto;
	}

	public async Task<bool> CreateUserAsync(UserDto userDto)
	{
		var user = _mapper.Map<User>(userDto);

		var isSuccess = await _userRepository.CreateAsync(user);
		if (!isSuccess)
			return false;

		_cachingRepository.Remove(CacheCode.User);

		return true;
	}

	public async Task<bool> UpdateUserAsync(UserDto userDto)
	{
		var userExists = await _userRepository.GetByIdAsync(userDto.Id);

		if (userExists == null)
			return false;

		var user = _mapper.Map<User>(userDto);

		var isSuccess = await _userRepository.UpdateAsync(user);
		if (!isSuccess) return false;

		_cachingRepository.Remove(CacheCode.User);

		return true;
	}

	public async Task<bool> DeleteUserAsync(int id)
	{
		var user = await _userRepository.GetByIdAsync(id);

		if (user == null)
			return false;

		var isSuccess = await _userRepository.DeleteAsync(id);
		if (!isSuccess) return false;

		_cachingRepository.Remove(CacheCode.User);

		return true;
	}
}
