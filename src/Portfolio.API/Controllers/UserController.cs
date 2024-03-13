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
		var hashedPassword = _tokenManager.HashPassword(userDto.Password);

		var user = new UserDto
		{
			Name = userDto.Name,
			NickName = userDto.NickName,
			Password = hashedPassword
		};

		await _userService.Create(user);

		return Created("api/register", Responses.SuccessMessage(hashedPassword));
	}

	[HttpPost]
	[Route("api/login")]
	public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
	{
		var user = await _userService.GetUserByNickName(loginViewModel.NickName);

		if (user is null)
			return BadRequest(Responses.NotFoundErrorMessage("User not found!"));

		var isPasswordCorrect = _tokenManager.ComparePasswords(loginViewModel.Password, user.Password);

		if (!isPasswordCorrect)
			return StatusCode(StatusCodes.Status401Unauthorized, Responses.UnauthorizedErrorMessage());

		var token = _tokenManager.GenerateToken(user);
		user.Password = null;

		var result = new ResultLoginViewModel { User = user, Token = token };

		return Ok(Responses.SuccessLoginMessage(result));
	}
}
