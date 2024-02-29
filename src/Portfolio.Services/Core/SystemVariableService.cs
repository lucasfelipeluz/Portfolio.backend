using AutoMapper;
using Portfolio.Core.Enums;
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

	public async Task<List<SystemVariableDto>> GetAllSystemVariablesAsync()
	{
		var cache = _cachingRepository.Get<List<SystemVariableDto>>(CacheCode.SystemVariable);

		if (cache != null)
		{
			return cache;
		}

		var systemVariables = await _systemVariablesRepository.GetAllAsync();
		var systemVariablesDto = _mapper.Map<List<SystemVariableDto>>(systemVariables);

		_cachingRepository.Save(CacheCode.SystemVariable, systemVariablesDto);

		return systemVariablesDto;
	}

	public async Task<SystemVariableDto> GetSystemVariableAsync(string key)
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

		var systemVariables = await _systemVariablesRepository.GetVariableAsync(key);
		var systemVariableDto = _mapper.Map<SystemVariableDto>(systemVariables);

		return systemVariableDto;
	}

	public async Task<bool> UpdateSystemVariableAsync(SystemVariableDto systemVariableDto)
	{
		var isSystemVariableExists = await _systemVariablesRepository.GetVariableAsync(systemVariableDto.Name);

		var systemVariable = _mapper.Map<SystemVariable>(systemVariableDto);

		if (isSystemVariableExists is null)
		{
			var isCreateSuccess = await _systemVariablesRepository.CreateAsync(systemVariable);
			if (!isCreateSuccess) return false;

			_cachingRepository.Remove(CacheCode.SystemVariable);

			return true;
		}

		var isUpdateSuccess = await _systemVariablesRepository.UpdateAsync(systemVariable);
		if (!isUpdateSuccess) return false;

		_cachingRepository.Remove(CacheCode.SystemVariable);

		return true;
	}
}
