using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Auth;
using Portfolio.API.Utils;
using Portfolio.API.ViewModels;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IUserService _userService;
	private readonly ITokenManager _tokenManager;

	public UserController(IUserService userService, IMapper mapper, ITokenManager tokenManager)
	{
		_userService = userService;
		_mapper = mapper;
		_tokenManager = tokenManager;
	}

	[HttpPost]
	[Route("api/register")]
	public async Task<IActionResult> Register([FromBody] RegisterViewModel userDto)
	{
		try
		{
			var hashedPassword = _tokenManager.HashPassword(userDto.Password);

			var user = new UserDto
			{
				Name = userDto.Name,
				NickName = userDto.NickName,
				Password = hashedPassword
			};

			await _userService.CreateUserAsync(user);

			return Created("api/register", Responses.SuccessMessage(hashedPassword));
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}

	[HttpPost]
	[Route("api/login")]
	public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
	{
		try
		{
			var user = await _userService.GetUserByNickNameAsync(loginViewModel.NickName);

			if (user == null)
				return BadRequest(Responses.NotFoundErrorMessage("User not found!"));

			var isPasswordCorrect = _tokenManager.ComparePasswords(loginViewModel.Password, user.Password);

			if (!isPasswordCorrect)
				return StatusCode(StatusCodes.Status401Unauthorized, Responses.UnauthorizedErrorMessage());

			var token = _tokenManager.GenerateToken(user);
			return Ok(new ResultViewModel { Message = token, Success = true });
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(StatusCodes.Status500InternalServerError, Responses.InternalServerErrorMessage());
		}
	}
}
