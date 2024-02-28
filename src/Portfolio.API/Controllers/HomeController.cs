using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Utils;
using Portfolio.API.ViewModels;
using Portfolio.Services.Interfaces;

namespace Portfolio.API.Controllers;

[Route("api/home")]
[ApiController]
public class HomeController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IProjectService _projectService;
	private readonly ISkillService _skillService;
	private readonly IAboutMeService _aboutMeService;
	private readonly IActivityService _activityService;

	public HomeController(
		IProjectService projectService,
		ISkillService skillService,
		IAboutMeService aboutMeService,
		IActivityService activityService,
		IMapper mapper
	)
	{
		_projectService = projectService;
		_skillService = skillService;
		_aboutMeService = aboutMeService;
		_activityService = activityService;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<IActionResult> GetAsync()
	{
		try
		{
			var projects = await _projectService.GetAllProjectsAsync();
			var skills = await _skillService.GetAllSkillsAsync();
			var aboutMe = await _aboutMeService.GetAboutMeAsync();
			var activities = await _activityService.GetAllActivitiesAsync();

			return Ok(
				new HomeViewModel
				{
					Projects = projects,
					Skills = skills,
					AboutMe = aboutMe,
					Activities = activities
				}
			);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpGet]
	[Route("project/{id}")]
	public async Task<IActionResult> GetProjectIdAsync(int id)
	{
		try
		{
			var project = await _projectService.GetProjectByIdAsync(id);
			return Ok(project);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpGet]
	[Route("skill/{id}")]
	public async Task<IActionResult> GetSkillByIdAsync(int id)
	{
		try
		{
			var skill = await _skillService.GetSkillByIdAsync(id);
			return Ok(skill);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}
}
