using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Dto;
using Portfolio.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Portfolio.API.Controllers
{
    [ApiController]
    [Route("conhecimento")]
    public class ConhecimentoController: ControllerBase
    {
        private readonly IConhecimentoService _conhecimentoService;

        public ConhecimentoController(IConhecimentoService conhecimentoService)
        {
            _conhecimentoService = conhecimentoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConhecimento()
        {
            try
            {
                ConhecimentoDto conhecimento = await _conhecimentoService.GetConhecimento();
                if (conhecimento == null)
                {
                    ConhecimentoDto model = new ConhecimentoDto
                    {
                        Id = 1,
                        Titulo = "Default",
                        AnosDeExperiencia = 0,
                        MesesDeExperiencia = 0,
                        Cor = "Default",
                        Icone = "Default",
                        Informacao = "Default",
                        DataAtualizacaoConhecimento = DateTime.Now
                    };
                    bool? respostaAddDefaultPerfil = await _conhecimentoService
                        .AddDefaultConhecimento(model);
                    if (respostaAddDefaultPerfil == null) { return Unauthorized("Erro ao adicionar o perfil!"); }
                    return Ok(model);
                }
                return Ok(conhecimento);
            }
            catch (Exception error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar os usuarios\n ERROR: {error.Message}");
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditPerfil(ConhecimentoDto model)
        {
            try
            {
                bool? responseEditPerfil = await _conhecimentoService
                    .UpdateConhecimento(model);
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
