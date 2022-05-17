using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Dto;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Identity;
using Portfolio.Persistence.Interfaces;

namespace Portfolio.Application
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersistence _userPersistence;

        public UsuarioService(UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                IMapper mapper,
                                IUserPersistence userPersistence)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userPersistence = userPersistence;
        }
        public async Task<SignInResult> CheckPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                User user = await _userManager.Users
                    .SingleOrDefaultAsync(user => user.UserName == userUpdateDto.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao tentar verificar a senha. Eroo: {erro.Message}");
            }
        }
        public async Task<UserUpdateDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    UserUpdateDto userToReturn = _mapper.Map<UserUpdateDto>(user);
                    return userToReturn;
                }

                return null;

            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao tentar criar a conta. Eroo: {erro.Message}");
            }
        }

        public Task<UserUpdateDto> GetUserByUsernameAsync(string username)
        {
            try
            {

            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao tentar pegar o usuário pelo nome de usuário. Eroo: {erro.Message}");
            }
        }

        public Task<bool> UserExists(string username)
        {
            try
            {

            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao tentar verificar se o usuário existe. Eroo: {erro.Message}");
            }
        }

        

        

        public Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {

            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao tentar atualizar a conta. Eroo: {erro.Message}");
            }
        }
    }
}
