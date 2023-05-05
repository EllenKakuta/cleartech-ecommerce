using AutoMapper;
using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos;
using Ellen_Falpus_CadCategoria.Data.Dtos.CarrinhoDeComprasDto;
using Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDoCarrinhoDto;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Ellen_Falpus_CadCategoria.Repository
{
    public class ProdutoDoCarrinhoRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly IMapper _mapper;

        public ProdutoDoCarrinhoRepository(EcommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CreateProdutoDoCarrinhoDto AdicionaProduto(CreateProdutoDoCarrinhoDto carrinhoDto)
        {
            ProdutoDoCarrinho prod = _mapper.Map<ProdutoDoCarrinho>(carrinhoDto);
            _context.Add(prod);
            _context.SaveChanges();
            return carrinhoDto;
        }

        public ProdutoDoCarrinho BuscaProdutoNoCarrinho(CreateProdutoDoCarrinhoDto dto)
        {
            var busca = _context.ProdutoDoCarrinho.FirstOrDefault(
                c => c.IdCarrinho == dto.IdCarrinho
                && c.IdProduto == dto.IdProduto);
            return busca;
        }

        public CreateProdutoDoCarrinhoDto Salvar(CreateProdutoDoCarrinhoDto dto)
        {
            _context.SaveChanges();
            return dto;
        }

        public ProdutoDoCarrinho BuscaProdutoNoCarrinhoPorId(int id)
        {
            return _context.ProdutoDoCarrinho.FirstOrDefault(p => p.Id == id);
        }

        public void DeletaProdutoNoCarrinho(int id)
        {
            var produtoNoCarrinho = BuscaProdutoNoCarrinhoPorId(id);
            _context.Remove(produtoNoCarrinho);
        }






    }
}
