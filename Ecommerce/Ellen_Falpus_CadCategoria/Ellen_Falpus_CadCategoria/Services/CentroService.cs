    using AutoMapper;
using Dapper.Contrib.Extensions;
using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos.CentroDto;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Models;
using Ellen_Falpus_CadCategoria.Repository;
using FluentResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ellen_Falpus_CadCategoria.Services
{
    public class CentroService
    {
        private readonly IMapper _mapper;
        private readonly IDbConnection _dbConnection;
        private readonly CentroRepository _repository;
        private readonly BuscaCEPService _buscaCEPService;
       

        public CentroService(IMapper mapper, IDbConnection dbConnection, CentroRepository centroRepository, BuscaCEPService buscaCEPService)
        {
            _mapper = mapper;
            _dbConnection = dbConnection;
            _repository = centroRepository;
            _buscaCEPService = buscaCEPService;
        }     

        public async Task<CentroDeDistribuicao> AdicionaCentro (CreateCentroDto centroDto)     

        {
            var map = await _buscaCEPService.BuscaCep(centroDto.CEP);
            CentroDeDistribuicao nome = _repository.NomeDocentro(centroDto);
            CentroDeDistribuicao endereco = _repository.Endereco(centroDto);
            if (nome != null && endereco != null)
            {
                throw new Exception();
            }


            centroDto.Localidade = map.Localidade;
            centroDto.Bairro = map.Bairro;
            centroDto.Logradouro = map.Logradouro;
            centroDto.UF = map.UF;

            CentroDeDistribuicao centroD = _mapper.Map<CentroDeDistribuicao>(centroDto);
            _repository.AdicionaCentro(centroD);
            return centroD;

        }

        public Result EditaCentro (int id, UpdateCentroDto centroDto)
        {
            CentroDeDistribuicao centro = _repository.RecuperarCentroPorId(id);
            if(centro == null)
            {
                return Result.Fail("Centro de Distribuição não encontrado");
            }
            List<Produto> produtos = _repository.BuscaProdutoPorId(id);
            if (produtos.Count > 0)
            {
                throw new ArgumentException();
            }
            _mapper.Map(centroDto, centro);
            _repository.EditarCentro(centro);
            return Result.Ok();

        }

        public ReadCentroDto PesquisaCentroPorId(int id)
        {

            CentroDeDistribuicao centro = _repository.RecuperarCentroPorId(id);

           if(centro != null)
            {
                ReadCentroDto centroDto= _mapper.Map<ReadCentroDto>(centro);
                return centroDto;
            }
           return null;
        }

           

        public List<CentroDeDistribuicao> FiltraCentro(string nome, bool? status, string cep, string logradouro, int? numero, string uf,
             string bairro, string localidade, string complemento, string ordem, int qtde, int pagina)
        {
            return _repository.FiltraCentro(nome, status, cep, logradouro, numero, uf, bairro,localidade,complemento, ordem, qtde, pagina);
        }

        public Result ApagaCentro(int id)
        {
            CentroDeDistribuicao centro = _repository.RecuperarCentroPorId(id);
            if(centro == null)
            {
                throw new Exception();
            }
            var apaga = _repository.ApagaCentro(centro);
            return Result.Ok();
           
        }

    }
}
