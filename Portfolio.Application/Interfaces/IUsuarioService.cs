using Microsoft.AspNetCore.Identity;
using Portfolio.Application.Dto;
using System.Threading.Tasks;

namespace Portfolio.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> UserExists(string username);
        Task<UserUpdateDto> GetUserByUsernameAsync(string username);
        Task<SignInResult> CheckPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserUpdateDto> CreateAccountAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);

    }
}
