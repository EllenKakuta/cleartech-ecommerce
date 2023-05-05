using AutoMapper;
using Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDto;
using Ellen_Falpus_CadCategoria.Modelos;

namespace Ellen_Falpus_CadCategoria.Profiles
{
    public class ProdutoProfile: Profile
    {
        public ProdutoProfile()
        {
            CreateMap<CreateProdutoDto, Produto>();
            CreateMap<Produto, ReadProdutoDto>();
            CreateMap<UpdateProdutoDto, Produto>();
        }
    }
}
 