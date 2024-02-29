using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface ISystemVariableService
{
	Task<List<SystemVariableDto>> GetAllSystemVariablesAsync();
	Task<SystemVariableDto> GetSystemVariableAsync(string key);
	Task<bool> UpdateSystemVariableAsync(SystemVariableDto systemVariableDto);
}
