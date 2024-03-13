using System.ComponentModel.Design;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
	public async Task<IActionResult> GetHomePublicAsync()
	{
		var projects = await _projectService.GetByIsActive(true);
		var skills = await _skillService.GetByIsActive(true);
		var aboutMe = await _aboutMeService.Get();
		var activities = await _activityService.Get();

		var homeViewModel = new HomeViewModel
		{
			Projects = projects,
			Skills = skills,
			AboutMe = aboutMe,
			Activities = activities
		};

		return Ok(homeViewModel);
	}

	[HttpGet]
	[Route("admin")]
	// [Authorize]
	public async Task<IActionResult> GetHomeAdminAsync(
		[FromQuery] bool? isActiveProject,
		[FromQuery] bool? isActiveSkill
	)
	{
		var projects = isActiveProject is null
			? await _projectService.Get()
			: await _projectService.GetByIsActive(isActiveProject.Value);

		var skills = isActiveSkill is null
			? await _skillService.Get()
			: await _skillService.GetByIsActive(isActiveSkill.Value);

		var activities = await _activityService.Get();
		var aboutMe = await _aboutMeService.Get();

		return Ok(
			new HomeViewModel
			{
				Projects = projects,
				Skills = skills,
				Activities = activities,
				AboutMe = aboutMe,
			}
		);
	}

	[HttpGet]
	[Route("project/{id}")]
	public async Task<IActionResult> GetProjectIdAsync(int id)
	{
		var project = await _projectService.GetById(id);
		return Ok(project);
	}

	[HttpGet]
	[Route("skill/{id}")]
	public async Task<IActionResult> GetSkillByIdAsync(int id)
	{
		var skill = await _skillService.GetById(id);
		return Ok(skill);
	}
}
