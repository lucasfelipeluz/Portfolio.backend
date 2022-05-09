using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Dto;
using Portfolio.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Portfolio.API.Controllers
{
    [ApiController]
    [Route("perfil")]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilService _perfilService;

        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPerfil()
        {
            try
            {
                PerfilDto perfil = await _perfilService.GetPerfil();
                if (perfil == null)
                {
                    PerfilDto model = new PerfilDto
                    {
                        Id = 1,
                        UrlEmail = "Default",
                        UrlInstagram = "Default",
                        UrlLinkedin = "Default",
                        UrlTelegram = "Default",
                        Descricao = "Default",
                        Cargo = "Default",
                        DataAtualizacaoPerfil = DateTime.Now
                    };
                    bool? respostaAddDefaultPerfil = await _perfilService
                        .AddDefaultPerfil(model);
                    if (respostaAddDefaultPerfil == null) { return Unauthorized("Erro ao adicionar o perfil!"); }
                    return Ok(model);
                }
                return Ok(perfil);
            }
            catch (Exception error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar os usuarios\n ERROR: {error.Message}");
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditPerfil(PerfilDto model)
        {
            try
            {
                bool? responseEditPerfil = await _perfilService
                    .UpdatePerfil(model);
                if (responseEditPerfil == null) { return Unauthorized("Projeto não encontrado!"); }
                return Ok("Perfil editado!");

            }
            catch (Exception error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar os usuarios\n ERROR: {error.Message}");
            }
        }


    }
}
