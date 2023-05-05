using System.ComponentModel.DataAnnotations;

namespace Usuarios.Data.Requests
{
    public class LoginUsuarioRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }    
    }
}
