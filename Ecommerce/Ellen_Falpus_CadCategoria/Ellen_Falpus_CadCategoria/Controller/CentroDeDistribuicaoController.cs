using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos.CentroDto;
using Ellen_Falpus_CadCategoria.Models;
using Ellen_Falpus_CadCategoria.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ellen_Falpus_CadCategoria.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CentroDeDistribuicaoController : ControllerBase
    {
        private CentroService _centroService;

        public CentroDeDistribuicaoController(CentroService centroservice)
        {
            _centroService = centroservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaCentro([FromBody] CreateCentroDto centroDto)
        {
            
            try
            {
                var readDto = await _centroService.AdicionaCentro(centroDto);
                return CreatedAtAction(nameof(FiltraCentro), new { nome = centroDto.Nome }, centroDto);
            }
            catch
            {
                return BadRequest("Centro de distribuição existente");
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditaCentro(int id, [FromBody] UpdateCentroDto centroDto)
        {
            try
            {
                Result resultado = _centroService.EditaCentro(id, centroDto);
                if (resultado.IsFailed) return NotFound();
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest("Não é possivel inativar um centro de distribuição com produtos ativos");
            }
        }

        [HttpGet("{id}")]
        public IActionResult PesquisaCentroPorId(int id)
        {
            ReadCentroDto centroDto = _centroService.PesquisaCentroPorId(id);
            if (centroDto == null) return NotFound();
            return Ok(centroDto);
        }



        //[HttpGet]
        //public IActionResult PesquisaCentro()
        //{
        //    ReadCentroDto centroDto = _centroService.();
        //    if (centroDto == null) return NotFound();
        //    return Ok(centroDto);
        //}



        [HttpGet("filtrar")]
        public List<CentroDeDistribuicao> FiltraCentro([FromQuery] string nome,
                                                       [FromQuery] bool? status,
                                                       [FromQuery] string cep,
                                                       [FromQuery] string logradouro,
                                                       [FromQuery] int? numero,
                                                       [FromQuery] string uf,
                                                       [FromQuery] string bairro,
                                                       [FromQuery] string localidade,
                                                       [FromQuery] string complemento,
                                                       [FromQuery] string ordem,
                                                       [FromQuery] int qtde,
                                                       [FromQuery] int pagina)
        {
            return _centroService.FiltraCentro(nome, status, cep, logradouro, numero, uf, bairro, localidade,
                complemento, ordem, qtde, pagina);
        }

        [HttpDelete("{id}")]
        public IActionResult ApagaCentro(int id)
        {
            try
            {
                var apaga = _centroService.ApagaCentro(id);
                return Ok();
            }

            catch
            {
                return BadRequest("Centro não encontrado");
            }
           
        }

    }      
}
