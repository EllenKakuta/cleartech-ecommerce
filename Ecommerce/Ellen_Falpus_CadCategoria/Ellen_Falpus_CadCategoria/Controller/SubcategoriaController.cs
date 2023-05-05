using AutoMapper;
using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos.SubcategoriaDto;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerilogTimings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ellen_Falpus_CadCategoria.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class SubcategoriaController : ControllerBase
    {

        private readonly SubcategoriaService _subcategoriaService;
        private readonly ILogger<SubcategoriaController> _logger;
        public SubcategoriaController(SubcategoriaService subcategoriaService, ILogger<SubcategoriaController> logger)
        {
            _subcategoriaService = subcategoriaService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AdicionaSub([FromBody] CreateSubcategoriaDto subcategoriaDto)
        {
            using (Operation.Time("---> Tempo para inclusão de uma nova subcategoria"))
            {
                try
                {
                    _logger.LogInformation("* POST ----> Requisição de inclusão de subcategorias através da controller ");
                    _logger.LogInformation("----> Objeto recebido {@subcategoriaDto}", subcategoriaDto);
                    if (!Regex.IsMatch(subcategoriaDto.Nome, @"^[a-zA-Zà-úÀ-Ú çÇ''\s]{3,50}$")) return StatusCode(400);
                    var Dto = _subcategoriaService.AdicionaSub(subcategoriaDto);               
                    return CreatedAtAction(nameof(PesquisaSubcategoria), new { nome = Dto.Nome }, Dto);
                }
                catch (ArgumentException ex)
                {
                    _logger.LogError(" ****** FALHA NA INCLUSÃO DA SUBCATEGORIA ****** ");
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(" ****** FALHA NA INCLUSÃO DA SUBCATEGORIA ****** ");
                    return BadRequest(ex.Message);
                }
            }

        }

        [HttpGet]
        public IActionResult PesquisaSubcategoria([FromQuery] bool? statusSub,
                                                 [FromQuery] string nomeSub,
                                                 [FromQuery] int qtdRegistroSub,
                                                 [FromQuery] string ordemSub)
        {
            using (Operation.Time("---> Tempo para execução da pesquisa "))
            {
                try
                {
                    _logger.LogInformation("* GET ----> Requisição de pesquisa das subcategorias através da controller ");
                    List<Subcategoria> subcategorias = _subcategoriaService.FiltraSub(statusSub, nomeSub, qtdRegistroSub, ordemSub);
                    return Ok(subcategorias);
                }
                catch (Exception ex)
                {
                    _logger.LogError(" ****** FALHA NA PESQUISA ****** ");
                    return BadRequest(ex.Message);
                }
            }

        }

        [HttpGet("{id}")]

        public IActionResult PesquisaSubPorId(int id)
        {
            using (Operation.Time("---> Tempo para execução da pesquisa "))
            {
                try
                {
                    _logger.LogInformation("* GET ----> Requisição de pesquisa das subcategorias pelo ID através da controller ");
                    _logger.LogInformation("----> Objeto recebido {@id}", id);
                    Subcategoria sub = _subcategoriaService.PesquisaSubPorId(id);
                    return Ok(sub);
                }
                catch (Exception ex)
                {
                    _logger.LogError(" ****** FALHA NA PESQUISA ****** ");
                    return BadRequest(ex.Message);
                }

            }

        }




        [HttpPut("{id}")]
        public IActionResult EditaSubcategoria(int id, [FromBody] UpdateSubcategoriaDto subcategoriaDto)
        {
            using (Operation.Time("---> Tempo para edição da subcategoria "))
            {
                try
                {
                    _logger.LogInformation("* PUT ----> Requisição de edição da subcategoria através da controller ");
                    _logger.LogInformation("----> Objeto recebido {@subcategoriaDto}", subcategoriaDto);
                    Result resultado = _subcategoriaService.EditaSubcategoria(id, subcategoriaDto);
                    return NoContent();
                }
                catch (ArgumentException ex)
                {
                    _logger.LogError(" ****** FALHA NA EDIÇÃO DA SUBCATEGORIA ****** ");
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(" ****** FALHA NA EDIÇÃO DA SUBCATEGORIA ****** ");
                    return BadRequest(ex.Message);
                }
            }

        }

        [HttpDelete("{id}")]
        public IActionResult ApagaSubcategoria(int id)
        {
            using (Operation.Time(" ---> Tempo para exclusão da categoria "))
            {
                try
                {
                    _logger.LogInformation("* DELETE ----> Requisição de exclusão da subcategoria através da controller ");
                    _logger.LogInformation("----> Objeto recebido {@id}", id);
                    Result resultado = _subcategoriaService.ApagaSubcategoria(id);
                    return NoContent();
                }

                catch (Exception ex)
                {
                    _logger.LogError(" ****** FALHA EXCLUSÃO DA SUBCATEGORIA ****** ");
                    return NotFound(ex.Message);

                }

            }

        }
    }
}
