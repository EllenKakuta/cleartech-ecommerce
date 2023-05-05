using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDto;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ellen_Falpus_CadCategoria.Services
{
    public class ProdutoService
    {
        private readonly EcommerceDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDbConnection _dbConnection;

        public ProdutoService(EcommerceDbContext context, IMapper mapper, IDbConnection dbConnection)
        {
            _context = context;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }


        public ReadProdutoDto AdicionaProduto(CreateProdutoDto produtoDto)
        {
            Produto produto = _context.Produtos.FirstOrDefault(produto => produto.Nome.ToLower() == produtoDto.Nome.ToLower());
            if (produto != null)
            {
                throw new ArgumentNullException();
            }

            Subcategoria sub = _context.Subcategorias.FirstOrDefault(sub => sub.Id == produtoDto.SubcategoriaId); 
           if(sub.Status == false)
            {
                throw new ArgumentException();
            }
           CentroDeDistribuicao centro = _context.CentrosDeDistribuicao.FirstOrDefault(centro => centro.Id == produtoDto.CentroId);
            if (centro.Id == null || centro.Status == false )
            {
                throw new Exception();
            }
            Produto prod = _mapper.Map<Produto>(produtoDto);
            _context.Produtos.Add(prod);
            _context.SaveChanges();
            return _mapper.Map<ReadProdutoDto>(prod);
        }
       
        public Result EditaProduto(int id, UpdateProdutoDto produtoDto)
        {

            Produto produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            if (produto == null)
            {
                return Result.Fail("Produto não encontrado");
            }

            _mapper.Map(produtoDto, produto);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result ApagaProduto(int id)
        {
            Produto produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            if (produto == null)
            {
                return Result.Fail("Produto não encontrado");
            }
            _context.Remove(produto);
            _context.SaveChanges();
            return Result.Ok();
        }

        public async Task<IReadOnlyList<Produto>> PesquisaProduto()
        {

            var result = await _dbConnection.GetAllAsync<Produto>();
            return result.ToList();
        }

        public async Task<Produto> PesquisaProdutoPorId(int id)
        {

            var result = await _dbConnection.GetAsync<Produto>(id);
            return result;
        }

        public List<Produto> FiltraProduto(string nome, double? peso, double? altura, double? largura,
                                          double? comprimento, double? valor, int? estoque, bool? status,
                                          string ordem, int qtde, int pagina)
        {
            var consulta = "SELECT * FROM Produtos WHERE";

            if (nome != null)
            {
                consulta += " Nome LIKE '%" + nome + "%' AND";
            }
            if (peso != null)
            {
                consulta += " Peso =@peso AND";
            }
            if (altura != null)
            {
                consulta += " Altura = @altura AND";
            }
            if (largura != null)
            {
                consulta += " Largura =@largura AND";
            }
            if (comprimento != null)
            {
                consulta += " Comprimento =@comprimento AND";
            }
            if (valor != null)
            {
                consulta += " Valor =@valor AND";
            }
            if (estoque != null)
            {
                consulta += " Estoque =@estoque AND";
            }
            if (status != null)
            {
                consulta += " Status = @status AND";
            }
            if (nome == null && peso == null
                && altura == null && largura == null && comprimento == null && valor == null && estoque == null && status == null)
            {
                var deleteWhere = consulta.LastIndexOf("WHERE");
                consulta = consulta.Remove(deleteWhere);
            }
            else
            {
                var deleteAnd = consulta.LastIndexOf("AND");
                consulta = consulta.Remove(deleteAnd);
            }
            if (ordem != null)
            {
                if (ordem.ToLower() == "up")
                {
                    consulta += "ORDER BY nome";
                }
                if (ordem.ToLower() == "down")
                {
                    consulta += "ORDER BY nome DESC";
                }
            }

            // Console.WriteLine(consulta);
            var result = _dbConnection.Query<Produto>(consulta, new
            {
                Nome = nome,
                Peso = peso,
                Altura = altura,
                Largura = largura,
                Comprimento = comprimento,
                Valor = valor,
                Estoque = estoque,
                Status = status,
            });
            if (qtde > 0 && pagina > 0)
            {
                var resultado = result.Skip((pagina - 1) * qtde).Take(qtde).ToList();
                return resultado;
            }
            return result.ToList();

        }



    }

}
