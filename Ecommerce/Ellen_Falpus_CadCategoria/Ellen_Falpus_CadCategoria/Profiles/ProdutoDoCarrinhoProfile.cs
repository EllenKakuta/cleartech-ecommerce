using AutoMapper;
using Ellen_Falpus_CadCategoria.Data.Dtos.CarrinhoDeComprasDto;
using Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDoCarrinhoDto;
using Ellen_Falpus_CadCategoria.Models;

namespace Ellen_Falpus_CadCategoria.Profiles
{
    public class ProdutoDoCarrinhoProfile : Profile
    {

        public ProdutoDoCarrinhoProfile()
        {
            CreateMap<CreateProdutoDoCarrinhoDto, ProdutoDoCarrinho>();
            CreateMap<ProdutoDoCarrinho, ReadProdutoDoCarrinhoDto>();
            CreateMap<UpdateProdutoDoCarrinhoDto, ProdutoDoCarrinho>();
        }
    }
}
