using AutoMapper;
using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos;
using Ellen_Falpus_CadCategoria.Data.Dtos.CategoriaDto;
using Ellen_Falpus_CadCategoria.Interfaces;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Services;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ellen_Falpus_CadCategoria.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriaRepository> _logger;

        public CategoriaRepository(EcommerceDbContext context, IMapper mapper, ILogger<CategoriaRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public void AdicionaCategoria(CreateCategoriaDto categoriaDto)
        {
            _logger.LogInformation("-> Validação repository adição de categoria ");
            Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
            _context.Add(categoria);
            _context.SaveChanges();
        }

        public Result ApagaCategoria(Categoria categoria)
        {
            _logger.LogInformation("-> Validação repository exclusão de categoria");
            _context.Remove(categoria);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result EditaCategoria(Categoria categoria, UpdateCategoriaDto categoriaDto)
        {
            _logger.LogInformation("-> Validação repository edição de categoria ");
            _mapper.Map(categoriaDto, categoria);
            _context.Update(categoria);
            _context.SaveChanges();
            return Result.Ok();

        }

        public Categoria PesquisaCategoriaPorId(int id)
        {
            _logger.LogInformation("-> Validação repository pesquisa categoria através do Id");
            Categoria cat = _context.Categorias.FirstOrDefault(cat => cat.Id == id);
            return cat;
        }

        public Categoria PesquisaNomeCategoria(CreateCategoriaDto categoriaDto)
        {
            _logger.LogInformation("-> Validação repository pesquisa categoria através do nome");
            Categoria cat = _context.Categorias.FirstOrDefault(cat => cat.Nome.ToLower() == categoriaDto.Nome.ToLower());
            return cat;
        }

        public List<Categoria> BuscarCategoria()
        {
            _logger.LogInformation("-> Validação repository para busca de categoria");
            List<Categoria> categorias = _context.Categorias.ToList();
            return categorias;
        }

        public IEnumerable<Subcategoria> BuscarSubId(int id)
        {
            _logger.LogInformation("-> Validação repository de busca subcategoria por Id");
            IEnumerable<Subcategoria> subcategorias = _context.Subcategorias.Where(subcategoria => subcategoria.CategoriaId == id);
            return subcategorias;
        }

        public List<Produto> BuscarProduto(int id)
        {
            _logger.LogInformation("-> Validação repository busca produto com Id de categoria e status ativos ");
            List<Produto> produtos = _context.Produtos.Where(produto => produto.CategoriaId == id && produto.Status == true).ToList();
            return produtos;
        }
  

        public List<Categoria>FiltraCategoria (bool? statusCateg, string nomeCateg, int qtdRegistroCateg, string ordemCateg)
        {
            _logger.LogInformation("-> Validação repository para filtro de categoria ");
            List<Categoria> categorias = _context.Categorias.ToList();
         
            if (statusCateg == true || statusCateg == false)
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               where categoria.Status == statusCateg
                                               select categoria;
                categorias = query.ToList();
            }

            if (!string.IsNullOrEmpty(nomeCateg))
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               where categoria.Nome.ToLower().StartsWith(nomeCateg.ToLower())
                                               select categoria;
                categorias = query.ToList();
            }
            if (qtdRegistroCateg > 0)
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                                  .Take(qtdRegistroCateg)
                                               select categoria;
                categorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordemCateg) && ordemCateg.ToLower() == "up")
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               orderby categoria.Nome ascending
                                               select categoria;
                categorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordemCateg) && ordemCateg.ToLower() == "down")
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               orderby categoria.Nome descending
                                               select categoria;
                categorias = query.ToList();
            }
            return categorias;
            
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
