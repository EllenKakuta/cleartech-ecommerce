using Ellen_Falpus_CadCategoria.Data.Dtos;
using Ellen_Falpus_CadCategoria.Data.Dtos.SubcategoriaDto;
using Ellen_Falpus_CadCategoria.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Testes.Serviço
{
    public class EcommerceRepository
    {
        public bool ValidaNome(string nome)
        {
            if (Regex.IsMatch(nome, "^[a-zA-Zà-úÀ-Ú çÇ''\\s]{1,50}$"))               
            {
                return true;
            }
            return false;
        }

        public bool Tamanho50(string nome)
        {
            if (nome.Count() <= 50)
            {
                return true;
            }
            return false;
        }

    }
    
   
}
