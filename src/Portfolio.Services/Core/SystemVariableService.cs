using AutoMapper;
using Portfolio.Core.Enums;
using Portfolio.Core.ExceptionHandles;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Cache;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services;

public class SystemVariableService : ISystemVariableService
{
	private readonly IMapper _mapper;
	private readonly ISystemVariablesRepository _systemVariablesRepository;
	private readonly ICachingRepository _cachingRepository;

	public SystemVariableService(
		IMapper mapper,
		ISystemVariablesRepository systemVariablesRepository,
		ICachingRepository cachingRepository
	)
	{
		_mapper = mapper;
		_systemVariablesRepository = systemVariablesRepository;
		_cachingRepository = cachingRepository;
	}

	public async Task<List<SystemVariableDto>> Get()
	{
		try
		{
			var cache = _cachingRepository.Get<List<SystemVariableDto>>(CacheCode.SystemVariable);

			if (cache is not null)
			{
				return cache;
			}

			var systemVariables = await _systemVariablesRepository.GetAllAsync();
			var systemVariablesDto = _mapper.Map<List<SystemVariableDto>>(systemVariables);

			_cachingRepository.Save(CacheCode.SystemVariable, systemVariablesDto);

			return systemVariablesDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<SystemVariableDto> GetByKey(string key)
	{
		try
		{
			var cache = _cachingRepository.Get<List<SystemVariableDto>>(CacheCode.SystemVariable);
			if (cache is not null)
			{
				var cacheSystemVariable = cache.Find(x => x.Name == key);

				if (cacheSystemVariable is not null)
				{
					return cacheSystemVariable;
				}
			}

			var systemVariables = await _systemVariablesRepository.GetByName(key);
			var systemVariableDto = _mapper.Map<SystemVariableDto>(systemVariables);

			return systemVariableDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<SystemVariableDto> Update(SystemVariableDto systemVariableDto)
	{
		try
		{
			var isSystemVariableExists = await GetByKey(systemVariableDto.Name);

			var systemVariable = _mapper.Map<SystemVariable>(systemVariableDto);

			if (isSystemVariableExists is null)
			{
				var createdSystemVariable = await _systemVariablesRepository.CreateAsync(systemVariable);

				_cachingRepository.Remove(CacheCode.SystemVariable);

				return _mapper.Map<SystemVariableDto>(createdSystemVariable);
			}

			var updatedSystemVariable = await _systemVariablesRepository.UpdateAsync(systemVariable);

			_cachingRepository.Remove(CacheCode.SystemVariable);

			return _mapper.Map<SystemVariableDto>(updatedSystemVariable);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}
}
