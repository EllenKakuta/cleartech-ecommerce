using AutoMapper;
using Ellen_Falpus_CadCategoria.Data.Dtos.CentroDto;
using Ellen_Falpus_CadCategoria.Models;

namespace Ellen_Falpus_CadCategoria.Profiles
{
    public class CentroProfile: Profile
    {
        public CentroProfile()
        {
            CreateMap<CreateCentroDto, CentroDeDistribuicao>();
            CreateMap<CentroDeDistribuicao, ReadCentroDto>();
            CreateMap<UpdateCentroDto, CentroDeDistribuicao>();
        }
    }
}
