using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Dto;
using Portfolio.Application.Interfaces;

namespace Portfolio.API.Controllers
{
    [ApiController]
    [Route("projeto")]
    public class ProjetoController : ControllerBase
    {
        
        private readonly IProjetoService _projetoService;

        public ProjetoController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjetos()
        {
            try
            {
                ProjetoDto[] projetos = await _projetoService.GetAllProjetos();
                if (projetos.Length == 0) { return NoContent(); }
                return Ok(projetos);

            }
            catch (Exception error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar os usuarios\n ERROR: {error.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjetosById(int id)
        {
            try
            {
                ProjetoDto projeto = await _projetoService.GetProjeto(id);
                if (projeto == null) { return NoContent(); }
                return Ok(projeto);

            }
            catch (Exception error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar os usuarios\n ERROR: {error.Message}");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProjeto(ProjetoDto model)
        {
            try
            {
                bool? responseAddProjeto = await _projetoService
                    .AddProjeto(model);
                if (responseAddProjeto == null) { return Unauthorized("Projeto não encontrado!"); }
                return Ok("Projeto adicionado!");

            }
            catch (Exception error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar os usuarios\n ERROR: {error.Message}");
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutProjeto(int id, ProjetoDto model)
        {
            try
            {
                bool? responseEditProjeto = await _projetoService
                    .UpdateProjeto(id, model);
                if (responseEditProjeto == null) { return Unauthorized("Projeto não encontrado!"); }
                return Ok("Projeto editado!");

            }
            catch (Exception error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar os usuarios\n ERROR: {error.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProjeto(int id)
        {
            try
            {
                bool? responseRemoveProjeto = await _projetoService
                    .DeleteProjeto(id);
                if (responseRemoveProjeto == null) { return Unauthorized("Projeto não encontrado!"); }
                return Ok($"Projeto com id {id} removido!");

            }
            catch (Exception error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar os usuarios\n ERROR: {error.Message}");
            }
        }
    }
}
