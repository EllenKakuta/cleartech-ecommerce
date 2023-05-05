using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Usuarios.Data.Requests;
using Usuarios.Services;

namespace Usuarios.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TrocaRoleUsuarioController : ControllerBase
     
    {
        private TrocaRoleUsuarioService _usuarioService;

        public TrocaRoleUsuarioController(TrocaRoleUsuarioService usuarioService)
        {
            _usuarioService= usuarioService;
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult TrocaRole (TrocaRoleUsuarioRequest request)
        {
            Result resultado = _usuarioService.TrocaRole (request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors.FirstOrDefault());
            return Ok("Permissão alterada com sucesso!");
        }
    }
}
