using AutoMapper;
using Ellen_Falpus_CadCategoria.Controller;
using Ellen_Falpus_CadCategoria.Data.Dtos;
using Ellen_Falpus_CadCategoria.Interfaces;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Repository;
using Ellen_Falpus_CadCategoria.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Testes.Serviço;
using Xunit;
using Xunit.Abstractions;

namespace Testes
{
    public class CategoriaTestes
    {
        public ITestOutputHelper _saidaConsole;
        readonly Mock <ICategoriaRepository> _categoriaRepository;
        readonly CategoriaService _categoriaService;


       

        public CategoriaTestes(ITestOutputHelper saidaConsole)
        {
            _saidaConsole = saidaConsole;
            _categoriaRepository = new Mock<ICategoriaRepository>();
            _categoriaService = new CategoriaService(new Mock<ICategoriaRepository>().Object);

        }
            

        [Fact]
        public void TesteCadastroCategoria()
        {
            var categoria = new CreateCategoriaDto
            {
                Nome = "TesteAdicionaCategoria"
            };
            var cadastro = _categoriaService.AdicionaCategoria(categoria);
            _saidaConsole.WriteLine("Categoria: " +cadastro.Nome +
                                    "\nData de criação: " + cadastro.DataCriacao+
                                    "\nStatus: "+cadastro.Status);
            Assert.NotNull(cadastro);
        }
       

        [Fact]
        public void TesteDataDeCriacao()
        {
            var categoria = new CreateCategoriaDto
            {
                Nome = "TesteAdicionaCategoria"
            };
            var data = $"{DateTime.Now:0001-01-01T00:00:00}";
           
            var cadastro = _categoriaService.AdicionaCategoria(categoria);

            Assert.Equal(data, $"{cadastro.DataCriacao:0001-01-01T00:00:00}");
        }

        [Fact]
        public void TesteStatusTrue()
        {
            
            var categoria = new CreateCategoriaDto()
            {
                Nome = "TesteAdicionaCategoria",
                Status = true
            };

           
            _categoriaService.AdicionaCategoria(categoria);
            
            Assert.True(categoria.Status);
        }

        [Fact]
        public void TesteStatusFalse()
        {
          
            var categoria = new CreateCategoriaDto()
            {
                Nome = "TesteAdicionaCategoria",
                Status = false
            };
           
             _categoriaService.AdicionaCategoria(categoria);
          
            Assert.False(categoria.Status);
        }



        [Fact]
        public void TesteCadastrarAte50caracteres()
        {
            var caracteres = new CreateCategoriaDto()
            {
                Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
               
            };            
            var controller = new CategoriaController(_categoriaService);
            var resultado = (ObjectResult)controller.AdicionaCategoria(caracteres);

            Assert.Equal(201, resultado.StatusCode);

        }

        [Fact]
        public void TesteCadastrarExcede50caracteres()
        {
            var caracteres = new CreateCategoriaDto()
            {
                Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",

            };
            var controller = new CategoriaController(_categoriaService);
            var resultado = (StatusCodeResult)controller.AdicionaCategoria(caracteres);

            Assert.Equal(400, resultado.StatusCode);
        }

        [Fact]
        public void TesteCadastrarVazio()
        {
            var caracteres = new CreateCategoriaDto()
            {
                Nome = "",

            };
            var controller = new CategoriaController(_categoriaService);
            var resultado = (StatusCodeResult)controller.AdicionaCategoria(caracteres);

            Assert.Equal(400, resultado.StatusCode);

        }

        [Fact]
        public void TesteCadastrarNumeros()
        {
            var caracteres = new CreateCategoriaDto()
            {
                Nome = "TesteAdicionaCategoria13",

            };
            var controller = new CategoriaController(_categoriaService);
            var resultado = (StatusCodeResult)controller.AdicionaCategoria(caracteres);

            Assert.Equal(400, resultado.StatusCode);
        }


        [Fact]
        public void TesteCadastrarCaracteresEspeciais()
        {
            var caracteres = new CreateCategoriaDto()
            {
                Nome = "TesteAdicionaCategoria#$%",

            };
            var controller = new CategoriaController(_categoriaService);
            var resultado = (StatusCodeResult)controller.AdicionaCategoria(caracteres);

            Assert.Equal(400, resultado.StatusCode);
        }

    }
}
