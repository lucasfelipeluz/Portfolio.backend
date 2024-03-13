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
		var projects = await _projectService.Get();

		return Ok(projects);
	}

	[HttpGet]
	[Route("{id}")]
	[Authorize]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		var project = await _projectService.GetById(id);

		if (project is null)
			return NotFound(Responses.NotFoundErrorMessage());

		return Ok(project);
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CreateAsync([FromBody] CreateProjectViewModel createProjectViewModel)
	{
		var projectDto = _mapper.Map<ProjectDto>(createProjectViewModel);
		var createdProject = await _projectService.Create(projectDto);

		if (createProjectViewModel.SkillsId is not null && createProjectViewModel.SkillsId.Length > 0)
		{
			int[] projectsId = { createdProject.Id };

			ProjectSkillOnDemandDto projectSkillOnDemandDto =
				new() { ProjectsId = projectsId, SkillsId = createProjectViewModel.SkillsId };

			await _projectSkillService.CreateOnDemand(projectSkillOnDemandDto);
		}

		return Created("/api/v1/projects", Responses.SuccessMessage("Project created successfully!"));
	}

	[HttpPut]
	[Authorize]
	public async Task<IActionResult> UpdateAsync([FromBody] UpdateProjectViewModel updateProjectViewModel)
	{
		var projectDto = _mapper.Map<ProjectDto>(updateProjectViewModel);
		await _projectService.Update(projectDto);

		return Ok(Responses.SuccessMessage("Project updated successfully!"));
	}

	[HttpDelete("{id}")]
	[Authorize]
	public async Task<IActionResult> DeleteAsync(int id)
	{
		await _projectService.Delete(id);

		return Ok(Responses.SuccessMessage("Project deleted successfully!"));
	}
}
