using Ellen_Falpus_CadCategoria.Data.Dtos.SubcategoriaDto;
using Ellen_Falpus_CadCategoria.Modelos;
using FluentResults;
using System;
using System.Collections.Generic;

namespace Ellen_Falpus_CadCategoria.Interfaces
{
    public interface ISubcategoriaRepository : IDisposable
    {
        Subcategoria AdicionaSub(Subcategoria subDto);
        Result ApagaSub(Subcategoria subcategoria);
        Result EditaSub(Subcategoria subcategoria, UpdateSubcategoriaDto subcategoriaDto);
        Subcategoria PesquisaSubPorId(int id);
        Subcategoria PesquisaNomeSub(CreateSubcategoriaDto subcategoriaDto);
        Categoria PesquisaCategoriaPorId(int id);
        List<Categoria> BuscarCategoria(int id);
        List<Subcategoria> BuscarSubcategoria();
        List<Produto> BuscarProduto(int id);
        List<Subcategoria> FiltraSub(bool? statusSub, string nomeSub, int qtdRegistroSub, string ordemSub);

        Categoria BuscaCategoriaPorId(Subcategoria subcategoria);

    }
}
