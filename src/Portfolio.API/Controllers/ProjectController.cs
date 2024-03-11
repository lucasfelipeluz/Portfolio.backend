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

	private readonly IProjectSkillService _projectSkillService;

	public ProjectController(IProjectService projectService, IMapper mapper, IProjectSkillService projectSkillService)
	{
		_projectService = projectService;
		_mapper = mapper;
		_projectSkillService = projectSkillService;
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

			if (project is null)
				return NotFound(Responses.NotFoundErrorMessage());

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
			var createdProject = await _projectService.CreateProjectAsync(projectDto, true);
			if (createdProject is null)
				return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());

			if (createProjectViewModel.SkillsId is not null && createProjectViewModel.SkillsId.Length > 0)
			{
				int[] projectsId = { createdProject.Id };

				ProjectSkillOnDemandDto projectSkillOnDemandDto =
					new() { ProjectsId = projectsId, SkillsId = createProjectViewModel.SkillsId };

				await _projectSkillService.CreateProjectSkillOnDemandAsync(projectSkillOnDemandDto);
			}

			return Created("/api/v1/projects", Responses.SuccessMessage("Project created successfully!"));
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
			var isSuccess = await _projectService.UpdateProjectAsync(projectDto);
			if (!isSuccess)
				return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());

			return Ok(Responses.SuccessMessage("Project updated successfully!"));
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
			var isSuccess = await _projectService.DeleteProjectAsync(id);
			if (!isSuccess)
				return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());

			return Ok(Responses.SuccessMessage("Project deleted successfully!"));
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}
}
