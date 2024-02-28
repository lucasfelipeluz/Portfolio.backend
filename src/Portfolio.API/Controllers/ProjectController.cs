using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Utils;
using Portfolio.API.ViewModels;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.API.Controllers;

[Route("api/projects")]
[ApiController]
public class ProjectController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IProjectService _projectService;

	public ProjectController(IProjectService projectService, IMapper mapper)
	{
		_projectService = projectService;
		_mapper = mapper;
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> GetAsync()
	{
		try
		{
			var projects = await _projectService.GetAllProjectsAsync();

			return Ok(projects);
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
			var project = await _projectService.GetProjectByIdAsync(id);
			return Ok(project);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CreateAsync([FromBody] CreateProjectViewModel createProjectViewModel)
	{
		try
		{
			var projectDto = _mapper.Map<ProjectDto>(createProjectViewModel);
			var response = await _projectService.CreateProjectAsync(projectDto);

			return Created("/api/v1/projects", response);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpPut]
	[Authorize]
	public async Task<IActionResult> UpdateAsync([FromBody] UpdateProjectViewModel updateProjectViewModel)
	{
		try
		{
			var projectDto = _mapper.Map<ProjectDto>(updateProjectViewModel);
			var response = await _projectService.UpdateProjectAsync(projectDto);
			return Ok(response);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpDelete("{id}")]
	[Authorize]
	public async Task<IActionResult> DeleteAsync(int id)
	{
		try
		{
			await _projectService.DeleteProjectAsync(id);
			return Ok();
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}
}
