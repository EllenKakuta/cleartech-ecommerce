using AutoMapper;
using Ellen_Falpus_CadCategoria.Data.Dtos.CarrinhoDeComprasDto;
using Ellen_Falpus_CadCategoria.Models;
using System.Linq;

namespace Ellen_Falpus_CadCategoria.Profiles
{
    public class CarrinhoDeComprasProfile : Profile
    {
        public CarrinhoDeComprasProfile()
        {
            CreateMap<CreateCarrinhoDto, CarrinhoDeCompras>();
            CreateMap<UpdateCarrinhoDeComprasDto, CarrinhoDeCompras>();
            CreateMap<CarrinhoDeCompras, ReadCarrinhoDeComprasDto>();
                // .ForMember(carrinho => carrinho.ProdutosCarrinho, opts => opts
                //.MapFrom(carrinho => carrinho.ProdutosCarrinho.Select(produtocarrinho => new
                //{
                //    produtocarrinho.Id,
                //    produtocarrinho.NomeProduto,
                //    produtocarrinho.QuantidadeProduto,
                //    produtocarrinho.ValorUnitario,
                //})));

                 
        }
    }
}
