 using AutoMapper;
using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos;
using Ellen_Falpus_CadCategoria.Data.Dtos.CategoriaDto;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerilogTimings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ellen_Falpus_CadCategoria.Controller

{

    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {

        private readonly CategoriaService _categoriaService;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(CategoriaService categoriaservice, ILogger<CategoriaController> logger)
        {
            _categoriaService = categoriaservice;
            _logger = logger;

        }


        [HttpPost]
        public IActionResult AdicionaCategoria([FromBody] CreateCategoriaDto categoriaDto)
        {
            using (Operation.Time("---> Tempo para inclusão de uma nova categoria"))

            {
                try
                {
                    _logger.LogInformation("* POST ----> Requisição de inclusão de categorias através da controller ");
                    _logger.LogInformation("----> Objeto recebido {@categoriaDto}", categoriaDto);
                    if (!Regex.IsMatch(categoriaDto.Nome, @"^[a-zA-Zà-úÀ-Ú çÇ''\s]{1,50}$")) return StatusCode(400);
                    CreateCategoriaDto Dto = _categoriaService.AdicionaCategoria(categoriaDto);
                    return CreatedAtAction(nameof(PesquisaCategoria), new { nome = Dto.Nome }, Dto);
                }
                catch (Exception ex) 
                {
                    _logger.LogError(" ****** FALHA NA INCLUSÃO DA CATEGORIA ****** ");

                    return BadRequest(ex.Message);

                }

            }


        }

        [HttpGet]
        public IActionResult PesquisaCategoria([FromQuery] bool? statusCateg,
                                                 [FromQuery] string nomeCateg,
                                                 [FromQuery] int qtdRegistroCateg,
                                                 [FromQuery] string ordemCateg)
        {
            using (Operation.Time("---> Tempo para execução da pesquisa "))
            {
                try
                {
                    _logger.LogInformation("* GET ----> Requisição de pesquisa das categorias através da controller ");
                    List<Categoria> categorias = _categoriaService.FiltraCategoria(statusCateg, nomeCateg, qtdRegistroCateg, ordemCateg);
                    return Ok(categorias);
                }
                catch(Exception ex)
                {
                    _logger.LogError(" ****** FALHA NA PESQUISA ****** ");
                    return BadRequest(ex.Message);
                }
            }
               
        }

        [HttpGet("{id}")]
        public IActionResult PesquisaCategoriaPorId(int id)
        {
            using (Operation.Time("---> Tempo para execução da pesquisa "))
            {
                try
                {
                    _logger.LogInformation("* GET ----> Requisição de pesquisa das categorias pelo ID através da controller ");
                    _logger.LogInformation("----> Objeto recebido {@id}", id);
                   Categoria cat = _categoriaService.PesquisaCategoriaPorId(id);
                    return Ok(cat);
                }
                catch(Exception ex)
                {
                    _logger.LogError(" ****** FALHA NA PESQUISA ****** ");
                    return BadRequest(ex.Message);
                }
            
            }

        }


        [HttpPut("{id}")]
        public IActionResult EditaCategoria(int id, [FromBody] UpdateCategoriaDto categoriaDto)
        {
            using (Operation.Time("---> Tempo para edição da categoria "))
            {

                try
                {
                    _logger.LogInformation("* PUT ----> Requisição de edição da categoria através da controller ");
                    _logger.LogInformation("----> Objeto recebido {@categoriaDto}", categoriaDto);
                    Result resultado = _categoriaService.EditaCategoria(id, categoriaDto);
                    return NoContent();
                }
                catch (ArgumentException ex)
                {
                    _logger.LogError(" ****** FALHA NA EDIÇÃO DA CATEGORIA ****** ");
                    return BadRequest(ex.Message);
                }

                catch (Exception ex)
                {
                    _logger.LogError(" ****** FALHA NA EDIÇÃO DA CATEGORIA ****** ");
                    return BadRequest(ex.Message);
                }
            }

        }


        [HttpDelete("{id}")]
        public IActionResult ApagaCategoria(int id)
        {
            using (Operation.Time("---> Tempo para exclusão da categoria "))
            {
                try
                {
                    _logger.LogInformation("* DELETE ----> Requisição de exclusão da categoria através da controller ");
                    _logger.LogInformation("----> Objeto recebido {@id}", id);
                    Result resultado = _categoriaService.ApagaCategoria(id);
                    return NoContent();
                }

                catch (Exception ex) 
                {
                    _logger.LogError(" ****** FALHA EXCLUSÃO DA CATEGORIA ****** ");
                    return NotFound(ex.Message);
                    
                }
                



            }



        }
    }
}
