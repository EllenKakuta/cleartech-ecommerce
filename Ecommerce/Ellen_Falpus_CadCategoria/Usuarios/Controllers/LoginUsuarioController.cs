using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Usuarios.Data.Requests;
using Usuarios.Services;

namespace Usuarios.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginUsuarioController: ControllerBase
    {
        private LoginUsuarioService _loginService;
        public LoginUsuarioController (LoginUsuarioService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogaUsuario (LoginUsuarioRequest request)
        {
            Result resultado = _loginService.LogaUsuario(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors.FirstOrDefault());
            return Ok(resultado.Successes.FirstOrDefault());

        }










    }
}
