using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Utils;
using Portfolio.API.ViewModels;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.API.Controllers;

[ApiController]
[Route("status")]
public class SystemVariableController : ControllerBase
{
	private readonly ISystemVariableService _systemVariableService;
	private readonly IMapper _mapper;

	public SystemVariableController(ISystemVariableService systemVariableService, IMapper mapper)
	{
		_systemVariableService = systemVariableService;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<IActionResult> GetAsync()
	{
		var systemVariables = await _systemVariableService.Get();

		return Ok(systemVariables);
	}

	[HttpGet]
	[Route("{name}")]
	public async Task<IActionResult> GetByNameAsync(string name)
	{
		var systemVariable = await _systemVariableService.GetByKey(name);
		if (systemVariable is null)
			return NotFound(Responses.NotFoundErrorMessage($"System variable {name} not found!"));

		return Ok(systemVariable);
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateOrUpdateSystemVariableViewModel createSystemVariableViewModel
	)
	{
		var systemVariableDto = _mapper.Map<SystemVariableDto>(createSystemVariableViewModel);
		await _systemVariableService.Update(systemVariableDto);

		return Created("/api/v1/status", Responses.SuccessMessage("System variable created with success!"));
	}
}
