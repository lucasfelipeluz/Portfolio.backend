using AutoMapper;
using Portfolio.Core.Enums;
using Portfolio.Core.ExceptionHandles;
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

	public async Task<List<UserDto>> Get()
	{
		try
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
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<UserDto> GetUserById(int id)
	{
		try
		{
			var cache = _cachingRepository.Get<List<UserDto>>(CacheCode.User);

			if (cache is not null)
			{
				var cacheUser = cache.Find(x => x.Id == id);

				if (cacheUser is not null)
					return cacheUser;
			}

			var user = await _userRepository.GetByIdAsync(id);
			var userDto = _mapper.Map<UserDto>(user);

			return userDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<UserDto> GetUserByNickName(string nickName)
	{
		try
		{
			var cache = _cachingRepository.Get<List<UserDto>>(CacheCode.User);

			if (cache is not null)
			{
				var cacheUser = cache.Find(x => x.NickName == nickName);

				if (cacheUser is not null)
					return cacheUser;
			}

			var user = await _userRepository.GetByNickName(nickName);
			var userDto = _mapper.Map<UserDto>(user);

			return userDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<UserDto> Create(UserDto userDto)
	{
		try
		{
			var user = _mapper.Map<User>(userDto);

			await _userRepository.CreateAsync(user);

			_cachingRepository.Remove(CacheCode.User);

			return userDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<UserDto> Update(UserDto userDto)
	{
		try
		{
			var userExists = await GetUserById(userDto.Id);

			if (userExists is null)
				throw new NotFoundEntityException("User not found");

			var user = _mapper.Map<User>(userDto);

			await _userRepository.UpdateAsync(user);

			_cachingRepository.Remove(CacheCode.User);

			return userDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<UserDto> Delete(int id)
	{
		try
		{
			var userDto = await GetUserById(id);

			if (userDto is null)
				throw new NotFoundEntityException("User not found");

			var user = _mapper.Map<User>(userDto);

			await _userRepository.DeleteAsync(user);

			_cachingRepository.Remove(CacheCode.User);

			return userDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}
}
