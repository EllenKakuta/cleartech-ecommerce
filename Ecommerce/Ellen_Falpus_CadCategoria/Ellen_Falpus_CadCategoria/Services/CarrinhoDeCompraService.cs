using AutoMapper;
using Ellen_Falpus_CadCategoria.Data.Dtos.CarrinhoDeComprasDto;
using Ellen_Falpus_CadCategoria.Data.Dtos.CentroDto;
using Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDoCarrinhoDto;
using Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDto;
using Ellen_Falpus_CadCategoria.Middleware.Exceptions;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Models;
using Ellen_Falpus_CadCategoria.Repository;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ellen_Falpus_CadCategoria.Services
{

    public class CarrinhoDeCompraService
    {
        private readonly IMapper _mapper;
        private readonly CarrinhoDeCompraRepository _carrinhoRepository;
        private readonly ProdutoDoCarrinhoRepository _produtoCarrinhoRepository;
        private readonly BuscaCEPService _buscaCEPService;


        public CarrinhoDeCompraService(IMapper mapper, CarrinhoDeCompraRepository carrinhorepository, ProdutoDoCarrinhoRepository produtoDoCarrinhoRepository,
            BuscaCEPService buscaCEPService)
        {
            _mapper = mapper;
            _carrinhoRepository = carrinhorepository;
            _produtoCarrinhoRepository = produtoDoCarrinhoRepository;
            _buscaCEPService = buscaCEPService;

        }


        public async Task<CarrinhoDeCompras> GeraCarrinho(CreateCarrinhoDto carrinhoDto)

        {
            var map = await _buscaCEPService.BuscaCep(carrinhoDto.CEP);
            carrinhoDto.Localidade = map.Localidade;
            carrinhoDto.Bairro = map.Bairro;
            carrinhoDto.Logradouro = map.Logradouro;
            carrinhoDto.UF = map.UF;

            CarrinhoDeCompras carrinho = _mapper.Map<CarrinhoDeCompras>(carrinhoDto);
            _carrinhoRepository.GeraCarrinho(carrinho);
            return carrinho;

        }

        public Result ApagaCarrinho(int id)
        {
            var carrinho = _carrinhoRepository.PesquisaCarrinhoPorId(id);
            if (carrinho == null)
            {
                return Result.Fail("Carrinho não encontrado");
            }
            return _carrinhoRepository.ApagaCarrinho(carrinho);

        }

        public CarrinhoDeCompras PesquisaCarrinhoPorId(int id)
        {
            return _carrinhoRepository.PesquisaCarrinhoPorId(id);
        }


        public List<CarrinhoDeCompras> PesquisaCarrinho()
        {
            return _carrinhoRepository.PesquisaCarrinho();
        }


        public CreateProdutoDoCarrinhoDto AdicionaProduto(CreateProdutoDoCarrinhoDto produtocarrinhoDto)
        {

            CarrinhoDeCompras pesquisacarrinho = IdentificaCarrinho(produtocarrinhoDto);

            Produto pesquisaproduto = IdentificaProdutoExistenteAtivo(produtocarrinhoDto);

            Mapeamento(produtocarrinhoDto, pesquisaproduto);

            _produtoCarrinhoRepository.AdicionaProduto(produtocarrinhoDto);

            AdicionarProdutoJaExistenteAoCarrinho(produtocarrinhoDto, pesquisacarrinho, pesquisaproduto);

            SalvaAlteracaoNoCarrinho(produtocarrinhoDto);

            return produtocarrinhoDto;

        }
     

        public ProdutoDoCarrinho RemoveProduto(CreateProdutoDoCarrinhoDto produtocarrinhoDto)
        {
            CarrinhoDeCompras pesquisacarrinho = IdentificaCarrinho(produtocarrinhoDto);

            Produto pesquisaproduto = IdentificaProdutoExistenteAtivo(produtocarrinhoDto);

            ProdutoDoCarrinho dentroDoCarrinho = VerificaSaldoDeProdutoNoCarrinhoParaRemover(produtocarrinhoDto);

            CalculaValorEQuantidade(produtocarrinhoDto, pesquisacarrinho, dentroDoCarrinho);

            _produtoCarrinhoRepository.DeletaProdutoNoCarrinho(dentroDoCarrinho.Id);

            SalvaAlteracaoNoCarrinho(produtocarrinhoDto);

            return dentroDoCarrinho;
        }



        private CarrinhoDeCompras IdentificaCarrinho(CreateProdutoDoCarrinhoDto produtocarrinhoDto)
        {
            var pesquisacarrinho = _carrinhoRepository.PesquisaCarrinhoPorId(produtocarrinhoDto.IdCarrinho);
            if (pesquisacarrinho == null)
            {
                throw new NullEx("Carrinho inexistente");
            }

            return pesquisacarrinho;
        }

        private ProdutoDoCarrinho VerificaSaldoDeProdutoNoCarrinhoParaRemover(CreateProdutoDoCarrinhoDto produtocarrinhoDto)
        {
            var dentroDoCarrinho = _produtoCarrinhoRepository.BuscaProdutoNoCarrinho(produtocarrinhoDto);
            try
            {


                dentroDoCarrinho.QuantidadeProduto -= produtocarrinhoDto.QuantidadeProduto;

                if (dentroDoCarrinho.QuantidadeProduto < produtocarrinhoDto.QuantidadeProduto)
                {
                    throw new NullEx("Quantidade informada maior que quantidade existente");
                }

        }
            catch(Exception ex)
            {
                throw new NullEx("Quantidade informada maior que quantidade existente");
    }
            return dentroDoCarrinho;
        }

        private static void CalculaValorEQuantidade(CreateProdutoDoCarrinhoDto produtocarrinhoDto, CarrinhoDeCompras pesquisacarrinho, ProdutoDoCarrinho dentroDoCarrinho)
        {
            pesquisacarrinho.ValorTotal -= (dentroDoCarrinho.ValorUnitario * produtocarrinhoDto.QuantidadeProduto);
            pesquisacarrinho.QuantidadeTotalProdutos -= produtocarrinhoDto.QuantidadeProduto;
        }

            private void SalvaAlteracaoNoCarrinho(CreateProdutoDoCarrinhoDto produtoDoCarrinhoDto)
            {
                _produtoCarrinhoRepository.Salvar(produtoDoCarrinhoDto);
                _carrinhoRepository.Salvar();
            }

        private void AdicionarProdutoJaExistenteAoCarrinho(CreateProdutoDoCarrinhoDto produtocarrinhoDto, CarrinhoDeCompras pesquisacarrinho, Produto pesquisaproduto)
        {
            var dentroDoCarrinho = _produtoCarrinhoRepository.BuscaProdutoNoCarrinho(produtocarrinhoDto);

            if (dentroDoCarrinho != null)
            {
                dentroDoCarrinho.QuantidadeProduto += produtocarrinhoDto.QuantidadeProduto;
                pesquisacarrinho.ValorTotal += (pesquisaproduto.Valor * produtocarrinhoDto.QuantidadeProduto);
                pesquisacarrinho.QuantidadeTotalProdutos += produtocarrinhoDto.QuantidadeProduto;
            }
        }

        private Produto IdentificaProdutoExistenteAtivo(CreateProdutoDoCarrinhoDto produtocarrinhoDto)
        {
            var pesquisaproduto = _carrinhoRepository.BuscaProdutoPorId(produtocarrinhoDto.IdProduto);
            if (pesquisaproduto == null)
            {
                throw new NullEx("Produto inexistente");
            }

            return pesquisaproduto;
        }
        private void Mapeamento(CreateProdutoDoCarrinhoDto produtocarrinhoDto, Produto pesquisaproduto)
        {
            var prodCar = _mapper.Map<CreateProdutoDoCarrinhoDto>(produtocarrinhoDto);
            prodCar.ValorUnitario = pesquisaproduto.Valor;
            prodCar.NomeProduto = pesquisaproduto.Nome;


        }

    }
}




  