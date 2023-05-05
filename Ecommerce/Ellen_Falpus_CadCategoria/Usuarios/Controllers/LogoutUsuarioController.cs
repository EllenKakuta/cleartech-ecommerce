using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Usuarios.Data.Requests;
using Usuarios.Services;

namespace Usuarios.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutUsuarioController: ControllerBase
    {
        private LogoutUsuarioService _logoutService;
        public LogoutUsuarioController(LogoutUsuarioService logoutService)
        {
            _logoutService = logoutService;
        }


        [HttpPost]

        public IActionResult DeslogaUsuario(LogoutUsuarioRequest request)
        {
            Result resultado = _logoutService.DeslogaUsuario(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors.FirstOrDefault());
            return Ok(resultado.Successes.FirstOrDefault());
        }
    }
}
