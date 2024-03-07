using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Utils;
using Portfolio.API.ViewModels;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.API.Controllers;

[Route("api/activities")]
[ApiController]
public class ActivityController : ControllerBase
{
	private readonly IActivityService _activityService;
	private readonly IMapper _mapper;

	public ActivityController(IActivityService activityService, IMapper mapper)
	{
		_activityService = activityService;
		_mapper = mapper;
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> GetAsync()
	{
		try
		{
			var activities = await _activityService.GetAllActivitiesAsync();

			return Ok(activities);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpGet]
	[Route("{id}")]
	[Authorize]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		try
		{
			var activity = await _activityService.GetActivityByIdAsync(id);

			if (activity == null)
			{
				return NotFound("Não foi encontrado nenhuma atividade com o id informado.");
			}

			return Ok(activity);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> PostAsync([FromBody] CreateActivityViewModel createActivityViewModel)
	{
		try
		{
			var activityDto = _mapper.Map<ActivityDto>(createActivityViewModel);

			var isSuccess = await _activityService.CreateActivityAsync(activityDto);
			if (!isSuccess)
				return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());

			return Created("api/v1/activity", Responses.SuccessMessage("Activity created successfully!"));
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpPut]
	[Authorize]
	public async Task<IActionResult> PutAsync([FromBody] UpdateActivityViewModel updateActivityViewModel)
	{
		try
		{
			var activityDto = _mapper.Map<ActivityDto>(updateActivityViewModel);

			var isSuccess = await _activityService.UpdateActivityAsync(activityDto);
			if (!isSuccess)
				return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());

			return Ok(Responses.SuccessMessage("Activity updated successfully!"));
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpDelete]
	[Route("{id}")]
	[Authorize]
	public async Task<IActionResult> DeleteAsync(int id)
	{
		try
		{
			var isSuccess = await _activityService.DeleteActivityAsync(id);
			if (!isSuccess)
				return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());

			return Ok(Responses.SuccessMessage("Activity deleted successfully!"));
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}
}
