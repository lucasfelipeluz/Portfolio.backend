using Portfolio.Services.Dto;

namespace Portfolio.API.ViewModels;

public class ResultLoginViewModel
{
	public UserDto User { get; set; }
	public string Token { get; set; }
}
