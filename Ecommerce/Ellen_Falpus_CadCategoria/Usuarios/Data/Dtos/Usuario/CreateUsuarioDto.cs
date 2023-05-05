using System;
using System.ComponentModel.DataAnnotations;

namespace Usuarios.Data.Dtos.Usuario
{
    public class CreateUsuarioDto
    {

        [Required(ErrorMessage = "O nome de usuário é obrigatório")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Permitido o uso do mínimo de 3 e máximo de 250 caracteres")]
        [RegularExpression(@"^[a-zA-Z''\s]{3,250}$", ErrorMessage = "Permitido somente o uso de letras")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informação inválida, por favor verifique o dado inserido")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Senha não coincide com a digitada anteriormente")]
        public string Repassword { get; set; }
        [Required]
        public DateTime? DataNascimento { get; set; }
        [Required(ErrorMessage ="O CPF é obrigatório")]
        [RegularExpression(@"^[0-9\s]{11}$", ErrorMessage = "Necessário a utilização de 11 caracteres numéricos")]
        public string CPF { get; set; }
        [Required(ErrorMessage ="O CEP é obrigatório")]
        [RegularExpression(@"^[0-9\s]{8}$", ErrorMessage = "Necessário a utilização de 8 caracteres numéricos")]
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Localidade { get; set; }
        public string Bairro { get; set; }
        [Required(ErrorMessage ="O número é obrigatório")]
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string UF { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
