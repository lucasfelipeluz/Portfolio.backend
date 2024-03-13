using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface ISystemVariableService
{
	Task<List<SystemVariableDto>> Get();
	Task<SystemVariableDto> GetByKey(string key);
	Task<SystemVariableDto> Update(SystemVariableDto systemVariableDto);
}
