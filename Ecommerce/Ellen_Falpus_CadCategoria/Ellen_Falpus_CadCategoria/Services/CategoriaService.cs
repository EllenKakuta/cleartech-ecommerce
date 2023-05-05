using AutoMapper;
using Dapper.Contrib.Extensions;
using Ellen_Falpus_CadCategoria.Controller;
using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos;
using Ellen_Falpus_CadCategoria.Data.Dtos.CategoriaDto;
using Ellen_Falpus_CadCategoria.Interfaces;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Repository;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ellen_Falpus_CadCategoria.Services
{

    public class CategoriaService
    {

        
        private readonly ICategoriaRepository _repository;
        private readonly ILogger<CategoriaService> _logger;

        public CategoriaService(ICategoriaRepository repository, ILogger<CategoriaService> logger)
        {          
            _repository = repository;
           _logger= logger;
        }


        public CreateCategoriaDto AdicionaCategoria(CreateCategoriaDto categoriaDto)
        {
            _logger.LogInformation("--> Validação para inclusão de nova categoria através da service ");
            var cat = _repository.PesquisaNomeCategoria(categoriaDto);
            if (cat != null)
            {
                _logger.LogError(" ****** Identificação de duplicidade de nome ****** ");
                throw new Exception("Categoria existente");
            }      
            _repository.AdicionaCategoria(categoriaDto);
            return categoriaDto;
        }

        public Result ApagaCategoria(int id)
        {
            _logger.LogInformation("--> Validação para exclusão da categoria através da service ");
            var categoria = _repository.PesquisaCategoriaPorId(id);
            if (categoria == null)
            {
                _logger.LogError(" ****** Informação não localizada ****** ");
                throw new Exception("Categoria não encontrada");
            }
            return _repository.ApagaCategoria(categoria);
           

        }
     
        
        public Categoria PesquisaCategoriaPorId(int id)
        {
            _logger.LogInformation("--> Validação para pesquisa da categoria através da service ");
            var pesquisa = _repository.PesquisaCategoriaPorId(id);
            if(pesquisa == null)
            {
                _logger.LogError(" ****** Informação não localizada ****** ");
                throw new Exception("Categoria inexistente");
            }
            return pesquisa;
        }

 
      
       

        public Result EditaCategoria(int id, UpdateCategoriaDto categoriaDto)
        {
            _logger.LogInformation("--> Validação para edição da categoria através da service ");
            Categoria categoria = _repository.PesquisaCategoriaPorId(id);
            IEnumerable<Subcategoria> subcategorias = _repository.BuscarSubId(id);
            List<Produto> produtos = _repository.BuscarProduto(id);
            if (categoria == null)
            {
                _logger.LogError(" ****** Informação não localizada ****** ");
                throw new ArgumentException("Categoria não encontrada");
            }
            if (categoriaDto.Status == false)
            {

                foreach (var Subcategoria in subcategorias)
                {
                    if (produtos.Count > 0)
                    {
                        _logger.LogError(" ****** Validação de alteração de status ****** ");
                        throw new Exception("Não é possível inativar categoria com produtos ativos");
                    }
                    else
                        Subcategoria.Status = false;
                }
            }
            else if (categoriaDto.Status == true)
            {
                foreach (var Subcategoria in subcategorias)
                {
                    Subcategoria.Status = true;
                }
            }           
            return _repository.EditaCategoria( categoria, categoriaDto);
            
        }
        public Categoria PesquisaNomeCategoria(CreateCategoriaDto categoriaDto)
        {
            return _repository.PesquisaNomeCategoria(categoriaDto);
        }
        public List<Categoria> FiltraCategoria (bool? statusCateg, string nomeCateg, int qtdRegistroCateg, string ordemCateg)
        {
            var pesquisa = _repository.BuscarCategoria();
            if (pesquisa== null)
            {
                _logger.LogError(" ****** Informação não localizada ****** ");
                throw new Exception("Categoria não encontrada");
            }           
            return _repository.FiltraCategoria(statusCateg, nomeCateg, qtdRegistroCateg, ordemCateg);
           
        }

    }
}
