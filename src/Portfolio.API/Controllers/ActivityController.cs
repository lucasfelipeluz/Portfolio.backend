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
		var activities = await _activityService.Get();

		return Ok(activities);
	}

	[HttpGet]
	[Route("{id}")]
	[Authorize]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		var activity = await _activityService.GetById(id);

		if (activity is null)
			return NotFound(Responses.NotFoundErrorMessage("Activity not found!"));

		return Ok(activity);
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> PostAsync([FromBody] CreateActivityViewModel createActivityViewModel)
	{
		var activityDto = _mapper.Map<ActivityDto>(createActivityViewModel);

		await _activityService.Create(activityDto);

		return Created("api/v1/activity", Responses.SuccessMessage("Activity created successfully!"));
	}

	[HttpPut]
	[Authorize]
	public async Task<IActionResult> PutAsync([FromBody] UpdateActivityViewModel updateActivityViewModel)
	{
		var activityDto = _mapper.Map<ActivityDto>(updateActivityViewModel);

		await _activityService.Update(activityDto);

		return Ok(Responses.SuccessMessage("Activity updated successfully!"));
	}

	[HttpDelete]
	[Route("{id}")]
	[Authorize]
	public async Task<IActionResult> DeleteAsync(int id)
	{
		await _activityService.Delete(id);

		return Ok(Responses.SuccessMessage("Activity deleted successfully!"));
	}
}
