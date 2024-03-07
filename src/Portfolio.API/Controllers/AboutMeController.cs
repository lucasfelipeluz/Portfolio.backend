using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Utils;
using Portfolio.API.ViewModels;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.API.Controllers;

[Route("api/about_me")]
[ApiController]
public class AboutMeController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IAboutMeService _aboutMeService;

	public AboutMeController(IMapper mapper, IAboutMeService aboutMeService)
	{
		_mapper = mapper;
		_aboutMeService = aboutMeService;
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> GetAsync()
	{
		try
		{
			var aboutMe = await _aboutMeService.GetAboutMeAsync();

			return Ok(aboutMe);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CreateAsync([FromBody] CreateAboutMeViewModel createAboutMeViewModel)
	{
		try
		{
			var aboutMeDto = _mapper.Map<AboutMeDto>(createAboutMeViewModel);
			var isSuccess = await _aboutMeService.UpdateAboutMeAsync(aboutMeDto);

			if (!isSuccess)
				return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());

			return Created("api/about_me", Responses.SuccessMessage("About me updated successfully!"));
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}
}
