using System;
using System.ComponentModel.DataAnnotations;

namespace Usuarios.Data.Dtos.Usuario
{
    public class UpdateUsuarioDto
    {
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Permitido o uso do mínimo de 3 e máximo de 250 caracteres")]
        [RegularExpression(@"^[a-zA-Z''\s]{3,250}$", ErrorMessage = "Permitido somente o uso de letras")]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Informação inválida, por favor verifique o dado inserido")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Senha não coincide com a digitada anteriormente")]
        public string Repassword { get; set; }
        public DateTime? DataNascimento { get; set; }
      
        [RegularExpression(@"^[0-9\s]{11}$", ErrorMessage = "Necessário a utilização de 11 caracteres numéricos")]
        public string CPF { get; set; }
        
        [RegularExpression(@"^[0-9\s]{8}$", ErrorMessage = "Necessário a utilização de 8 caracteres numéricos")]
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Localidade { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string UF { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataModificacao { get; set; } = DateTime.Now;
    }
}
