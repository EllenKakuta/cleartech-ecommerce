using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Usuarios.Data.Dtos.Usuario;
using Usuarios.Data.Requests;
using Usuarios.Models;
using Usuarios.Services;

namespace Usuarios.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroUsuarioController : ControllerBase 
    {
        private CadastroUsuarioService _cadastroService;

        public CadastroUsuarioController(CadastroUsuarioService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost("/cadastrausuario")]
        //[Authorize(Roles ="regular")]
        public async Task<IActionResult> CadastraUsuario([FromBody]CreateUsuarioDto createDto)
        {

            Result resultado = await _cadastroService.CadastraUsuario(createDto);
            if (resultado.IsFailed) return BadRequest(resultado.Errors.FirstOrDefault());
            return Ok(resultado.Successes.FirstOrDefault());
        }

        [HttpPost("/ativa")]
        public IActionResult AtivaUsuario(AtivaContaRequest request)
        {
            Result resultado = _cadastroService.AtivaUsuario(request);
            if (resultado.IsFailed) return StatusCode(500);
            return Ok(resultado.Successes.FirstOrDefault());
               
        }

        [HttpGet("/pesquisa")]
        [Authorize(Roles = "admin,lojista")]
        public async Task<IActionResult> ListaUsuario([FromQuery] bool? status,
                                                      [FromQuery] string username,
                                                      [FromQuery] string cpf,
                                                      [FromQuery] string email)
        {
            List<ReadUsuarioDto> resultado = await _cadastroService.ListaUsuario(status, username, cpf, email);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin, regular, lojista")]
        public async Task<IActionResult> EditaUsuario(int id,[FromBody] UpdateUsuarioDto dto)
        {
            try
            {
                Result resultado = await _cadastroService.EditaUsuario(id, dto);
                return Ok();
            }
            catch(ArgumentException)
            {
                return BadRequest("Usuário não encontrado");
            }
        }



        [HttpPost("/cadastralojista")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CadastraLojista([FromBody] CreateUsuarioDto createDto)
        {

            Result resultado = await _cadastroService.CadastraLojista(createDto);
            if (resultado.IsFailed) return BadRequest(resultado.Errors.FirstOrDefault());
            return Ok(resultado.Successes.FirstOrDefault());
        }

    }
}
        
    

