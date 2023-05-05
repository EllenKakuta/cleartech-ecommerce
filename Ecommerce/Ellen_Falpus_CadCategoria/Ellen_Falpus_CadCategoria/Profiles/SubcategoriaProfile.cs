using AutoMapper;
using Ellen_Falpus_CadCategoria.Data.Dtos.SubcategoriaDto;
using Ellen_Falpus_CadCategoria.Modelos;
using System.Linq;

namespace Ellen_Falpus_CadCategoria.Profiles
{
    public class SubcategoriaProfile : Profile
    {
        public SubcategoriaProfile()
        {
            CreateMap<CreateSubcategoriaDto, Subcategoria>();
            CreateMap<UpdateSubcategoriaDto, Subcategoria>();
            CreateMap<Subcategoria, ReadSubcategoriaDto>()
                //.ForMember(subcategoria => subcategoria.Categoria, opts => opts
                //.MapFrom(subcategoria => subcategoria.Categoria.Select(categoria => new
                //{
                //    categoria.Id,
                //    categoria.Nome,
                //})))
                .ForMember(subcategoria => subcategoria.Produtos, opts => opts
                .MapFrom(subcategoria => subcategoria.Produtos.Select(produto => new
                {
                    produto.Id,
                    produto.Nome,
                    produto.Status,
                                    
                })));
                


        }
    }
}
