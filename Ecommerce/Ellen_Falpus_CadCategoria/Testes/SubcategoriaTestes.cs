using AutoMapper;
using Ellen_Falpus_CadCategoria.Controller;
using Ellen_Falpus_CadCategoria.Data.Dtos;
using Ellen_Falpus_CadCategoria.Data.Dtos.SubcategoriaDto;
using Ellen_Falpus_CadCategoria.Interfaces;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Profiles;
using Ellen_Falpus_CadCategoria.Repository;
using Ellen_Falpus_CadCategoria.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Testes
{
    public class SubcategoriaTestes
    {
        public ITestOutputHelper _saidaConsole;
        readonly Mock<ISubcategoriaRepository> _subcategoriaRepository;
      
        readonly SubcategoriaService _subcategoriaService;
        readonly IMapper _mapper;
      



        public SubcategoriaTestes(ITestOutputHelper saidaConsole)
        {
     
            _saidaConsole = saidaConsole;
            _subcategoriaRepository = new Mock<ISubcategoriaRepository>();
            _subcategoriaService = new SubcategoriaService(_subcategoriaRepository.Object, new Mock<IMapper>().Object);

        }


        [Fact]
        public void TesteCadastroSubcategoria()
        {
            _subcategoriaRepository.Setup(x => x.AdicionaSub(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "Teste", DataCriacao = DateTime.Now, Status = true, CategoriaId =1 });
            _subcategoriaRepository.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true });
           
            var subcategoria = _subcategoriaService.AdicionaSub(new CreateSubcategoriaDto()
            {
                Nome = "TesteAdicionaSubcategoria"

            });
            _saidaConsole.WriteLine("Subcategoria: " + subcategoria.Nome +
                                    "\nData de criação: " + subcategoria.DataCriacao +
                                    "\nStatus: " + subcategoria.Status +
                                    "\nCategoriaId: " + subcategoria.CategoriaId);
            Assert.NotNull(subcategoria);
        }

        [Fact]
        public void TesteDataDeCriacao()
        {
            _subcategoriaRepository.Setup(x => x.AdicionaSub(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "TesteAdicionaSubcategoria", DataCriacao = DateTime.Now, Status = true });
            _subcategoriaRepository.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true });
                       
            var data = $"{DateTime.Now:0001-01-01T00:00:00}";

            var subcategoria = _subcategoriaService.AdicionaSub(new CreateSubcategoriaDto()
            {
                Nome = "TesteAdicionaSubcategoria"
            });

            Assert.Equal(data, $"{subcategoria.DataCriacao:0001-01-01T00:00:00}");
        }


        [Fact]
        public void TesteStatusTrue()
        {
            _subcategoriaRepository.Setup(x => x.AdicionaSub(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "TesteAdicionaSubcategoria", DataCriacao = DateTime.Now, Status = true });
            _subcategoriaRepository.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true });
          
            var subcategoria = _subcategoriaService.AdicionaSub(new CreateSubcategoriaDto()
            {
                Nome = "TesteAdicionaSubcategoria",
                Status = true,
            });

            Assert.True(subcategoria.Status);
        }


        [Fact]
        public void TesteStatusFalse()
        {
            _subcategoriaRepository.Setup(x => x.AdicionaSub(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "TesteAdicionaSubcategoria", DataCriacao = DateTime.Now, Status = false });
            _subcategoriaRepository.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true });
            
            var subcategoria = _subcategoriaService.AdicionaSub(new CreateSubcategoriaDto()
            {
                Nome = "TesteAdicionaSubcategoria",
                Status = false,
            });

            Assert.False(subcategoria.Status);
        }


        [Theory]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 1)]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaa", 1)]
        [InlineData("aaa",1)]
        public void TesteCadastrar03Ate50caracteres(string nome, int categoriaId)
        {
            _subcategoriaRepository.Setup(x => x.AdicionaSub(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "TesteAdicionaSubcategoria", DataCriacao = DateTime.Now, Status = true });
            _subcategoriaRepository.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = true });
            
            var controller = new SubcategoriaController(_subcategoriaService);

            var resultado = (ObjectResult)controller.AdicionaSub(new CreateSubcategoriaDto()
            {
                Nome = nome,
                CategoriaId = categoriaId
            });

            Assert.Equal(201, resultado.StatusCode);
        }


        [Fact]
        public void TesteCadastrarExcede50caracteres()
        {
            var caracteres = new CreateSubcategoriaDto()
            {
                Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                CategoriaId= 1
            };
            var controller = new SubcategoriaController(_subcategoriaService);
            var resultado = (StatusCodeResult)controller.AdicionaSub(caracteres);

            Assert.Equal(400, resultado.StatusCode);
        }

        [Theory]
        [InlineData("aa", 1)]
        [InlineData("", 1)]       
        public void TesteCadastrarMenosQue03Caracteres(string nome, int categoriaId)
        {
            var caracteres = new CreateSubcategoriaDto()
            {
                Nome = nome,
                CategoriaId= categoriaId

            };
            var controller = new SubcategoriaController(_subcategoriaService);
            var resultado = (StatusCodeResult)controller.AdicionaSub(caracteres);

            Assert.Equal(400, resultado.StatusCode);
        }

        [Fact]
        public void TesteCadastrarNumeros()
        {
            var caracteres = new CreateSubcategoriaDto()
            {
                Nome = "TesteAdicionaSubcategoria13",
                CategoriaId= 1

            };
            var controller = new SubcategoriaController(_subcategoriaService);
            var resultado = (StatusCodeResult)controller.AdicionaSub(caracteres);

            Assert.Equal(400, resultado.StatusCode);
        }

        [Fact]
        public void TesteCadastrarCaracteresEspeciais()
        {
            var caracteres = new CreateSubcategoriaDto()
            {
                Nome = "TesteAdicionaSubcategoria#$%",

            };
            var controller = new SubcategoriaController(_subcategoriaService);
            var resultado = (StatusCodeResult)controller.AdicionaSub(caracteres);

            Assert.Equal(400, resultado.StatusCode);
        }

        [Fact]
        public void TesteCadastrarCategoriaInativa()
        {
            _subcategoriaRepository.Setup(x => x.AdicionaSub(It.IsAny<Subcategoria>())).Returns(new Subcategoria() { Nome = "TesteAdicionaSubcategoria", DataCriacao = DateTime.Now, Status = true });
            _subcategoriaRepository.Setup(x => x.BuscaCategoriaPorId(It.IsAny<Subcategoria>())).Returns(new Categoria() { Status = false });

            Assert.Throws<Exception>(
                () => _subcategoriaService.AdicionaSub(new CreateSubcategoriaDto() { }));
        }

    }
}
