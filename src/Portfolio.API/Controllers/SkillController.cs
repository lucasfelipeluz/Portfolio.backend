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

	public SkillController(IMapper mapper, ISkillService skillService)
	{
		_mapper = mapper;
		_skillService = skillService;
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> GetAsync()
	{
		try
		{
			var skills = await _skillService.GetAllSkillsAsync();

			return Ok(skills);
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
			var skill = await _skillService.GetSkillByIdAsync(id);

			if (skill is null)
				return NotFound(Responses.NotFoundErrorMessage());

			return Ok(skill);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpPost]
	public async Task<IActionResult> CreateAsync([FromBody] CreateSkillViewModel createSkillViewModel)
	{
		try
		{
			var skillDto = _mapper.Map<SkillDto>(createSkillViewModel);
			var isSuccess = await _skillService.CreateSkillAsync(skillDto);
			if (!isSuccess)
				return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());

			return Created("api/skills", Responses.SuccessMessage("Skill created with success!"));
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpPut]
	[Authorize]
	public async Task<IActionResult> UpdateAsync([FromBody] UpdateSkillViewModel updateSkillViewModel)
	{
		try
		{
			var skillDto = _mapper.Map<SkillDto>(updateSkillViewModel);
			var isSuccess = await _skillService.UpdateSkillAsync(skillDto);
			if (!isSuccess)
				return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());

			return Ok(Responses.SuccessMessage("Skill updated with success!"));
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
			var isSuccess = await _skillService.DeleteSkillAsync(id);
			if (!isSuccess)
				return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());

			return Ok(Responses.SuccessMessage("Skill deleted with success!"));
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}
}
