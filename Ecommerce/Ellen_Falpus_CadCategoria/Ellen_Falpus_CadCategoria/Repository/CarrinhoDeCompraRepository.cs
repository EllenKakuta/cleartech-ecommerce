using AutoMapper;
using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDoCarrinhoDto;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Models;
using FluentResults;
using Org.BouncyCastle.Asn1.Nist;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Ellen_Falpus_CadCategoria.Repository
{
    public class CarrinhoDeCompraRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly IMapper _mapper;

        public CarrinhoDeCompraRepository(EcommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
            
        public void GeraCarrinho (CarrinhoDeCompras carrinhoDeCompras)
        {
            _context.Add(carrinhoDeCompras);
            _context.SaveChanges();
        }
      

        public Result ApagaCarrinho(CarrinhoDeCompras carrinhoDeCompras)
        {
            _context.Remove(carrinhoDeCompras);
            _context.SaveChanges();
            return Result.Ok();
        }

        public CarrinhoDeCompras PesquisaCarrinhoPorId(int id)
        {
            return _context.CarrinhoDeCompras.FirstOrDefault(car => car.Id == id);
           
        }


        public List<CarrinhoDeCompras> PesquisaCarrinho()   
        {
            List<CarrinhoDeCompras> carrinho = _context.CarrinhoDeCompras.ToList();
            return carrinho;
        }
       

        public List<Produto> BuscaProdutoPorIdAtivo(int id)
        {
            List<Produto> produtos = _context.Produtos.Where(produto => produto.Id == id && produto.Status == true).ToList();
            return produtos;
        }

       

        public Produto BuscaProdutoPorId (int id)
        {
            Produto produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            return produto;
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
       
    }




}

