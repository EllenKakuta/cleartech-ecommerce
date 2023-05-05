using Ellen_Falpus_CadCategoria.Data.Dtos;
using Ellen_Falpus_CadCategoria.Data.Dtos.SubcategoriaDto;
using Ellen_Falpus_CadCategoria.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Serviço
{
    internal interface IEcommerceRepository
    {
        public List<CreateCategoriaDto> BuscaCategorias();
        public List<CreateSubcategoriaDto> BuscaSubcategorias();
    }
}
