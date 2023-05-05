using Ellen_Falpus_CadCategoria.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ellen_Falpus_CadCategoria.Data.Dtos.CarrinhoDeComprasDto
{
    public class CreateCarrinhoDto
    {
        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve conter 8 algarismos")]
        [RegularExpression("^(([0-9])*)$", ErrorMessage = "Permitido somente o uso de números")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "O campo número é obrigatório")]
        public int Numero { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public int QuantidadeTotalProdutos { get; set; }
        public double ValorTotal { get; set; }

    }
}
