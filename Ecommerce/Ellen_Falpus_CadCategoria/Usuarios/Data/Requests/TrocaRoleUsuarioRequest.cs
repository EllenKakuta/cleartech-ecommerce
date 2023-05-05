using System.ComponentModel.DataAnnotations;

namespace Usuarios.Data.Requests
{
    public class TrocaRoleUsuarioRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
