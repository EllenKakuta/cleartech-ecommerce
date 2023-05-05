using AutoMapper;
using Dapper.Contrib.Extensions;
using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos.SubcategoriaDto;
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
    public class SubcategoriaService
    {
       
        readonly IMapper _mapper;
        private readonly ISubcategoriaRepository _repository;
        private readonly ILogger<SubcategoriaService> _logger;


        public SubcategoriaService(ISubcategoriaRepository repository, IMapper mapper, ILogger<SubcategoriaService> logger)
        {

            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            
        }
        public Subcategoria AdicionaSub(CreateSubcategoriaDto subcategoriaDto)
        {
            _logger.LogInformation("--> Validação para inclusão de nova subcategoria através da service ");
            Subcategoria subcategoria = _mapper.Map<Subcategoria>(subcategoriaDto);
            var subcat = _repository.PesquisaNomeSub(subcategoriaDto);
            if (subcat != null)
            {
                _logger.LogError(" ****** Identificação de duplicidade de nome ****** ");
                throw new ArgumentException("Subcategoria existente");
            }
            var s = _repository.BuscaCategoriaPorId(subcategoria);
            if (s.Status == false)
            {
                _logger.LogError(" ****** Identificação de impossibilidade de cadastro ****** ");
                throw new Exception("Categoria inativa, cadastro de subcategoria não autorizado");
            }
            return _repository.AdicionaSub(subcategoria);
            
            }

        public Result EditaSubcategoria(int id, UpdateSubcategoriaDto subcategoriaDto)
        {
            _logger.LogInformation("--> Validação para edição da subcategoria através da service ");
            Subcategoria subcategoria = _repository.PesquisaSubPorId(id);
            if (subcategoria == null)
            {
                _logger.LogError(" ****** Informação não localizada ****** ");
                throw new ArgumentException("Subcategoria não encontrada");
            }
            List<Produto> produtos = _repository.BuscarProduto(id);
            if (produtos.Count > 0)
            {
                _logger.LogError(" ****** Validação de alteração de status ****** ");
                throw new Exception("Não é possível inativar uma subcategoria com produto ativo");
            }
            
            return _repository.EditaSub(subcategoria,subcategoriaDto);
            
        }

        public Result ApagaSubcategoria(int id)
        {
            _logger.LogInformation("--> Validação para exclusão da subcategoria através da service ");
            Subcategoria subcategoria = _repository.PesquisaSubPorId(id);
            if (subcategoria == null)
            {
                _logger.LogError(" ****** Informação não localizada ****** ");
                throw new Exception("Subcategoria não encontrada");
            }
            return _repository.ApagaSub(subcategoria);
           

        }

          public Subcategoria PesquisaSubPorId(int id)
          {
            _logger.LogInformation("--> Validação para pesquisa da subcategoria através da service ");
            var pesquisa = _repository.PesquisaSubPorId(id);
            if (pesquisa == null)
            {
                _logger.LogError(" ****** Informação não localizada ****** ");
                throw new Exception("Subcategoria inexistente");
            }
            return pesquisa;
          }

        public List<Subcategoria> FiltraSub(bool? statusSub, string nomeSub, int qtdRegistroSub, string ordemSub)
        {
            var pesquisa = _repository.BuscarSubcategoria();
            if (pesquisa == null)
            {
                _logger.LogError(" ****** Informação não localizada ****** ");
                throw new Exception("Subcategoria não encontrada");
            }
            return _repository.FiltraSub(statusSub, nomeSub, qtdRegistroSub, ordemSub);
        }

    }
}
