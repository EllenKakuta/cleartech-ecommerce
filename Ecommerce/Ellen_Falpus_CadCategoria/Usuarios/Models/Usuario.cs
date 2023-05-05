using System;
using System.ComponentModel.DataAnnotations;

namespace Usuarios.Models
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório")]
        [StringLength(250)]
        [RegularExpression(@"^[a-zA-Z''\s]${3,250}", ErrorMessage = "Permitido o uso do mínimo de 3 e máximo de 250 caracteres, somente o uso de letras")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informação inválida, por favor verifique o dado inserido")]
        public string Email { get; set; }

        public DateTime? DataNascimento { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [RegularExpression(@"^[0-9\s]{11}$", ErrorMessage = "Necessário a utilização de 11 caracteres numéricos, permitido somente o uso de números")]
        public string CPF { get; set; }
 
        
        [Required(ErrorMessage = "O CEP é obrigatório")]
        [RegularExpression(@"^[0-9\s]{8}$", ErrorMessage = "Necessário a utilização de 8 caracteres numéricos")]
        public string CEP { get; set; }
        
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }

        [Required(ErrorMessage = "O número é obrigatório")]
        public int Numero { get; set; }

        public string Complemento { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataModificacao { get; set; }
    }
}
