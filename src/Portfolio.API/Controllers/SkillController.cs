using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Utils;
using Portfolio.API.ViewModels;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.API.Controllers;

[Route("api/skills")]
[ApiController]
public class SkillController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly ISkillService _skillService;
	private readonly IProjectSkillService _projectSkillService;

	public SkillController(IMapper mapper, ISkillService skillService, IProjectSkillService projectSkillService)
	{
		_mapper = mapper;
		_skillService = skillService;
		_projectSkillService = projectSkillService;
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> GetAsync()
	{
		var skills = await _skillService.Get();

		return Ok(skills);
	}

	[HttpGet]
	[Route("{id}")]
	[Authorize]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		var skill = await _skillService.GetById(id);

		if (skill is null)
			return NotFound(Responses.NotFoundErrorMessage());

		return Ok(skill);
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CreateAsync([FromBody] CreateSkillViewModel createSkillViewModel)
	{
		var skillDto = _mapper.Map<SkillDto>(createSkillViewModel);
		var createdSkill = await _skillService.Create(skillDto);

		if (createSkillViewModel.ProjectsId is not null && createSkillViewModel.ProjectsId.Length > 0)
		{
			int[] skillId = { createdSkill.Id };

			ProjectSkillOnDemandDto projectSkillOnDemandDto =
				new() { ProjectsId = createSkillViewModel.ProjectsId, SkillsId = skillId };

			await _projectSkillService.CreateOnDemand(projectSkillOnDemandDto);
		}

		return Created("api/skills", Responses.SuccessMessage("Skill created with success!"));
	}

	[HttpPut]
	[Authorize]
	public async Task<IActionResult> UpdateAsync([FromBody] UpdateSkillViewModel updateSkillViewModel)
	{
		var skillDto = _mapper.Map<SkillDto>(updateSkillViewModel);
		await _skillService.Update(skillDto);

		return Ok(Responses.SuccessMessage("Skill updated with success!"));
	}

	[HttpDelete("{id}")]
	[Authorize]
	public async Task<IActionResult> DeleteAsync(int id)
	{
		await _skillService.Delete(id);

		return Ok(Responses.SuccessMessage("Skill deleted with success!"));
	}
}
