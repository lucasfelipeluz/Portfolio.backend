using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Services.Dto;

namespace Portfolio.API.Auth
{
  public class TokenManager : ITokenManager
  {
    private readonly IConfiguration _configuration;

    public TokenManager(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    
    public string HashPassword(string value)
    {
      if (string.IsNullOrWhiteSpace(value)) return null;

      var hashedData = BCrypt.Net.BCrypt.HashPassword(value);

      return hashedData;
    }

    public bool ComparePasswords(string password, string value)
    {
      if (string.IsNullOrWhiteSpace(password)) return false;
      if (string.IsNullOrWhiteSpace(value)) return false;

      var isValid = BCrypt.Net.BCrypt.Verify(password, value);

      return isValid;
    }

    public string GenerateToken(UserDto userDto)
    {
      var tokenHandler = new JwtSecurityTokenHandler();

      var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, userDto.Name),
                    new Claim(ClaimTypes.Role, userDto.IsActive != null && userDto.IsActive == true ? "active" : "disable")
          }),
        Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"])),

        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}