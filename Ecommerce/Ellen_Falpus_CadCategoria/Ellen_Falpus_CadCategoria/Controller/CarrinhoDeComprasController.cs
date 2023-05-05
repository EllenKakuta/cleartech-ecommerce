using Ellen_Falpus_CadCategoria.Data.Dtos.CarrinhoDeComprasDto;
using Ellen_Falpus_CadCategoria.Data.Dtos.CentroDto;
using Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDoCarrinhoDto;
using Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDto;
using Ellen_Falpus_CadCategoria.Models;
using Ellen_Falpus_CadCategoria.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ellen_Falpus_CadCategoria.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CarrinhoDeComprasController : ControllerBase
    {
        private CarrinhoDeCompraService _carrinhoService;

        public CarrinhoDeComprasController(CarrinhoDeCompraService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        [HttpPost]
        public async Task<CarrinhoDeCompras> GeraCarrinho([FromBody] CreateCarrinhoDto carrinhoDto)
        {
            return await _carrinhoService.GeraCarrinho(carrinhoDto);

        }

        [HttpDelete("{id}")]
        public IActionResult ApagaCarrinho(int id)
        {

            Result resultado = _carrinhoService.ApagaCarrinho(id);
            if (resultado.IsFailed) return NotFound("Carrinho não encontrado");
            return NoContent();

        }

        [HttpGet("{id}")]
        public IActionResult PesquisaCarrinhoPorId(int id)
        {
            var car = _carrinhoService.PesquisaCarrinhoPorId(id);
            if (car == null) return NotFound("Carrinho não encontrado");
            return Ok(car);
        }

        [HttpGet]
        public IActionResult PesquisaCarrinho()
        {
            var carrinho = _carrinhoService.PesquisaCarrinho();
            if (carrinho == null) return NotFound();
            return Ok(carrinho);
        }



        [HttpPut("adicionar")]
        public IActionResult AdicionarProduto([FromBody] CreateProdutoDoCarrinhoDto Dto)
        {
            var resultado = _carrinhoService.AdicionaProduto(Dto);
            if (resultado == null) return BadRequest();
            return Ok(resultado);

        }

        [HttpPut("remover")]
        public IActionResult RemoveProduto(CreateProdutoDoCarrinhoDto Dto)
        {
            var resultado = _carrinhoService.RemoveProduto(Dto);
            if (resultado == null) return BadRequest();
            return Ok(resultado);
        }


    }
}
