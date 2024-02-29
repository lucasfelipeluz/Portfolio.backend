using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Utils;
using Portfolio.API.ViewModels;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.API.Controllers;

[Route("api/project_skill")]
[ApiController]
public class ProjectSkillController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IProjectSkillService _projectSkillService;

	public ProjectSkillController(IProjectSkillService projectSkillService, IMapper mapper)
	{
		_projectSkillService = projectSkillService;
		_mapper = mapper;
	}

	[HttpPost]
	//[Authorize]
	public async Task<IActionResult> CreateAsync([FromBody] CreateProjectSkillViewModel createProjectSkillViewModel)
	{
		try
		{
			var projectSkillDto = _mapper.Map<ProjectSkillDto>(createProjectSkillViewModel);
			var isSuccess = await _projectSkillService.CreateProjectSkillAsync(projectSkillDto);
			if (!isSuccess) return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());

			return Created("/api/v1/projects", Responses.SuccessMessage("Relationship created"));
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}
}
