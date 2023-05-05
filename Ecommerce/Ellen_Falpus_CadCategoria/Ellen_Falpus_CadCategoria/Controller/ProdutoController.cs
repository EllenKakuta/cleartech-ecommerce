using AutoMapper;
using Dapper;
using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDto;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ellen_Falpus_CadCategoria.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;
       
      

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
            
        }

        [HttpPost]
        public IActionResult AdicionaProduto([FromBody] CreateProdutoDto produtoDto)
        {
            try
            {
                ReadProdutoDto readDto = _produtoService.AdicionaProduto(produtoDto);

                return CreatedAtAction(nameof(PesquisaProdutoPorId), new { Id = readDto.Id }, readDto);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Produto existente");
            }
            catch (ArgumentException)
            {
                return BadRequest("Só é possível o cadastro de produtos em subcategorias ativas");
            }
           catch (Exception)
            {
                return BadRequest("O ID de um centro de distribuição ativo é obrigatório");
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> PesquisaProduto()
        {
            var resultado = await _produtoService.PesquisaProduto();
            if(resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpGet("filtrar")]

        public IActionResult FiltraProduto([FromQuery] string nome,
                                           [FromQuery] double? peso,
                                           [FromQuery] double? altura,
                                           [FromQuery] double? largura,
                                           [FromQuery] double? comprimento,
                                           [FromQuery] double? valor,
                                           [FromQuery] int? estoque,
                                           [FromQuery] bool? status,
                                           [FromQuery] string ordem,
                                           [FromQuery] int qtde,
                                           [FromQuery] int pagina)
        {
            var filtro = _produtoService.FiltraProduto(nome, peso, altura, largura, comprimento, valor, estoque, status,
                ordem, qtde, pagina);
            if (filtro == null) return NotFound();
            return Ok(filtro);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> PesquisaProdutoPorId(int id)
        {
            var data = await _produtoService.PesquisaProdutoPorId(id);
            if(data == null) return NotFound();
            return Ok(data);
        }


        [HttpPut("{id}")]
        public IActionResult EditaProduto(int id, [FromBody] UpdateProdutoDto produtoDto)
        {
            Result resultado = _produtoService.EditaProduto(id, produtoDto);
            if(resultado.IsFailed) return NotFound();
            return NoContent();
        }

         
        [HttpDelete("{id}")]
        public IActionResult ApagaProduto(int id)
        {
            Result resultado = _produtoService.ApagaProduto(id);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }


      
        
    }
}
  