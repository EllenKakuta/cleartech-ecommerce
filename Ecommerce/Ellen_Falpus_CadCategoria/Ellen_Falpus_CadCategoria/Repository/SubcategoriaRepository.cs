using AutoMapper;
using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos.SubcategoriaDto;
using Ellen_Falpus_CadCategoria.Interfaces;
using Ellen_Falpus_CadCategoria.Modelos;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ellen_Falpus_CadCategoria.Repository
{
    public class SubcategoriaRepository : ISubcategoriaRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<SubcategoriaRepository> _logger;
        public SubcategoriaRepository(EcommerceDbContext context, IMapper mapper, ILogger<SubcategoriaRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public Subcategoria AdicionaSub(Subcategoria subDto)
        {
            _logger.LogInformation("-> Validação repository adição de subcategoria ");
            _context.Subcategorias.Add(subDto);
            _context.SaveChanges();
            return subDto;
        }

        public Result ApagaSub(Subcategoria subcategoria)
        {
            _logger.LogInformation("-> Validação repository exclusão de subcategoria");
            _context.Remove(subcategoria);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result EditaSub(Subcategoria subcategoria, UpdateSubcategoriaDto subcategoriaDto)
        {
            _logger.LogInformation("-> Validação repository edição de subcategoria ");
            _mapper.Map(subcategoriaDto, subcategoria);
            _context.Update(subcategoria);
            _context.SaveChanges();
            return Result.Ok();

        }

        public Subcategoria PesquisaSubPorId(int id)
        {
            _logger.LogInformation("-> Validação repository pesquisa categoria através do Id");
            Subcategoria sub = _context.Subcategorias.FirstOrDefault(sub => sub.Id == id);
            return sub;
        }

        public Subcategoria PesquisaNomeSub(CreateSubcategoriaDto subcategoriaDto)
        {
            _logger.LogInformation("-> Validação repository pesquisa subcategoria através do nome");
            Subcategoria sub = _context.Subcategorias.FirstOrDefault(sub => sub.Nome.ToLower() == subcategoriaDto.Nome.ToLower());
            return sub;
        }

        public Categoria PesquisaCategoriaPorId (int id)
        {
            _logger.LogInformation("-> Validação repository pesquisa categoria através do Id");
            Categoria cat = _context.Categorias.FirstOrDefault(c => c.Id == id && c.Status == true);
            return cat;

        }
        public Categoria BuscaCategoriaPorId(Subcategoria subcategoria)
        {
            _logger.LogInformation("-> Validação repository buscar categoria através do Id");
            return _context.Categorias.FirstOrDefault(c => c.Id == subcategoria.CategoriaId);
        }


        public List<Categoria>BuscarCategoria(int id)
        {
            List<Categoria> cat = _context.Categorias.Where(c=> c.Id == id && c.Status ==true).ToList();
            return cat;
        }
        public List<Subcategoria> BuscarSubcategoria()
        {
            _logger.LogInformation("-> Validação repository para busca de subcategoria");
            List<Subcategoria> subcategorias = _context.Subcategorias.ToList();
            return subcategorias;
        }
               
        public List<Produto> BuscarProduto(int id)
        {
            _logger.LogInformation("-> Validação repository busca produto com Id de categoria e status ativos ");
            List<Produto> produtos = _context.Produtos.Where(produto => produto.CategoriaId == id && produto.Status == true).ToList();
            return produtos;
        }

        public List<Subcategoria> FiltraSub(bool? statusSub, string nomeSub, int qtdRegistroSub, string ordemSub)
        {
            _logger.LogInformation("-> Validação repository para filtro de categoria ");
            List<Subcategoria> subcategorias = _context.Subcategorias.ToList();
            if (subcategorias == null)
            {
                throw new ArgumentException();
            }
            if (statusSub == true || statusSub == false)
            {
                IEnumerable<Subcategoria> query = from subcategoria in subcategorias
                                                  where subcategoria.Status == statusSub
                                                  select subcategoria;
                subcategorias = query.ToList();
            }

            if (!string.IsNullOrEmpty(nomeSub))
            {
                IEnumerable<Subcategoria> query = from subcategoria in subcategorias
                                                  where subcategoria.Nome.ToLower().StartsWith(nomeSub.ToLower())
                                                  select subcategoria;
                subcategorias = query.ToList();
            }
            if (qtdRegistroSub > 0)
            {
                IEnumerable<Subcategoria> query = from subcategoria in subcategorias
                                                  .Take(qtdRegistroSub)
                                                  select subcategoria;
                subcategorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordemSub) && ordemSub.ToLower() == "up")
            {
                IEnumerable<Subcategoria> query = from subcategoria in subcategorias
                                                  orderby subcategoria.Nome ascending
                                                  select subcategoria;
                subcategorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordemSub) && ordemSub.ToLower() == "down")
            {
                IEnumerable<Subcategoria> query = from subcategoria in subcategorias
                                                  orderby subcategoria.Nome descending
                                                  select subcategoria;
                subcategorias = query.ToList();
            }
            List<ReadSubcategoriaDto> readSubDto = _mapper.Map<List<ReadSubcategoriaDto>>(subcategorias);

            return subcategorias.ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}
 
