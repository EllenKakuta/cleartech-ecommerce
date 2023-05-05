using Ellen_Falpus_CadCategoria.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ellen_Falpus_CadCategoria.Models
{
    public class CentroDeDistribuicao
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(128, MinimumLength = 3, ErrorMessage = "Permitido o uso de no mínimo 3 e no máximo 128 caracteres alfanuméricos")]
        public string Nome { get; set; }

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
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; }
        [JsonIgnore]
        public virtual List<Produto> Produtos { get; set; }

    }
}
