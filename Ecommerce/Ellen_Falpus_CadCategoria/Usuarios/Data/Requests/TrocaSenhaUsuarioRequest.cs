using System.ComponentModel.DataAnnotations;

namespace Usuarios.Data.Requests
{
    public class TrocaSenhaUsuarioRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]      
        [Compare("NewPassword", ErrorMessage = "Senha não coincide com a digitada anteriormente")]
        public string ReNewPassword { get; set; }
        
    }
}
