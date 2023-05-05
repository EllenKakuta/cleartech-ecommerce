using System;
using System.ComponentModel.DataAnnotations;

namespace Ellen_Falpus_CadCategoria.Data.Dtos.CentroDto
{
    public class UpdateCentroDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(128, MinimumLength = 3, ErrorMessage = "Permitido o uso de no mínimo 3 e no máximo 128 caracteres alfanuméricos")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório")]
        [RegularExpression(@"^[0-9\s]{8}$", ErrorMessage = "Necessário a utilização de 8 caracteres numéricos")]
        public string CEP { get; set; }
      
        [Required(ErrorMessage = "O campo número é obrigatório")]
        public int Numero { get; set; }

        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }  
        public string Localidade { get; set; }
        public string UF { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}
