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
		try
		{
			var systemVariables = await _systemVariableService.GetAllSystemVariablesAsync();

			return Ok(systemVariables);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpGet]
	[Route("{name}")]
	public async Task<IActionResult> GetByNameAsync(string name)
	{
		try
		{
			var systemVariable = await _systemVariableService.GetSystemVariableAsync(name);
			if (systemVariable is null)
				return NotFound(Responses.NotFoundErrorMessage());

			return Ok(systemVariable);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateOrUpdateSystemVariableViewModel createSystemVariableViewModel
	)
	{
		try
		{
			var systemVariableDto = _mapper.Map<SystemVariableDto>(createSystemVariableViewModel);
			var isSucess = await _systemVariableService.UpdateSystemVariableAsync(systemVariableDto);

			if (!isSucess)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
			}

			return Created("/api/v1/status", Responses.SuccessMessage("System variable created with success!"));
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}
}
