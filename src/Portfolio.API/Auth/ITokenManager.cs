using Portfolio.Services.Dto;

namespace Portfolio.API.Auth;

public interface ITokenManager
{
	string HashPassword(string value);
	bool ComparePasswords(string password, string value);
	string GenerateToken(UserDto userDto);
}
