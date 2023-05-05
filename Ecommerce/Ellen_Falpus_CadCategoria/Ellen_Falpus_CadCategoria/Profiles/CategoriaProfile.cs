using AutoMapper;
using Ellen_Falpus_CadCategoria.Data.Dtos;
using Ellen_Falpus_CadCategoria.Data.Dtos.CategoriaDto;
using Ellen_Falpus_CadCategoria.Modelos;
using System.Linq;

namespace Ellen_Falpus_CadCategoria.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaDto, Categoria>();
            
            CreateMap<UpdateCategoriaDto,Categoria>();
            CreateMap<Categoria, ReadCategoriaDto>()
                .ForMember(categoria => categoria.Subcategorias, opts => opts
                .MapFrom(categoria => categoria.Subcategorias.Select(subcategoria => new
                {
                    subcategoria.Id,
                    subcategoria.Nome,
                    subcategoria.Status,
                    // subcategoria.CategoriaId,
                })))
                .ForMember(categoria => categoria.Produtos, opts => opts
                .MapFrom(categoria => categoria.Produtos.Select(produto => new
                {
                    produto.Id,
                    produto.Nome,
                    produto.Status,

                })));

        }
    }
}
