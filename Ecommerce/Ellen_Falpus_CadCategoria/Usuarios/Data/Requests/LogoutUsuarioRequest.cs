using System.ComponentModel.DataAnnotations;

namespace Usuarios.Data.Requests
{
    public class LogoutUsuarioRequest
    {
        [Required]
        public string Email { get; set; }

    }
}
