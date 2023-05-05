using Ellen_Falpus_CadCategoria.Data.Dtos;
using Ellen_Falpus_CadCategoria.Data.Dtos.CategoriaDto;
using Ellen_Falpus_CadCategoria.Modelos;
using FluentResults;
using System;
using System.Collections.Generic;

namespace Ellen_Falpus_CadCategoria.Interfaces
{
    public interface ICategoriaRepository : IDisposable

    {

        void AdicionaCategoria(CreateCategoriaDto categoriaDto);
        Result ApagaCategoria(Categoria categoria);
        Result EditaCategoria(Categoria categoria, UpdateCategoriaDto categoriaDto);
        Categoria PesquisaCategoriaPorId(int id);
        Categoria PesquisaNomeCategoria(CreateCategoriaDto categoriaDto);
        List<Categoria> BuscarCategoria();
        IEnumerable<Subcategoria> BuscarSubId(int id);
        List<Produto> BuscarProduto(int id);
        List<Categoria> FiltraCategoria(bool? statusCateg, string nomeCateg, int qtdRegistroCateg, string ordemCateg);
        
    }
}
