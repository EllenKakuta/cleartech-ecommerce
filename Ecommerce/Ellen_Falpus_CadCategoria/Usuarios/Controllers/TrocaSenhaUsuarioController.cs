using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Data.Requests;
using Usuarios.Services;

namespace Usuarios.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TrocaSenhaUsuarioController : ControllerBase
    {
        private TrocaSenhaUsuarioService _trocasenhaservice;

        public TrocaSenhaUsuarioController (TrocaSenhaUsuarioService trocasenhaservice)
        {
            _trocasenhaservice = trocasenhaservice;
        }

        [HttpPost]

        public IActionResult TrocaSenha(TrocaSenhaUsuarioRequest request)
        {
            Result resultado = _trocasenhaservice.TrocaSenha(request);
            if (resultado.IsFailed) return Unauthorized("Favor verificar dados inseridos");
            return Ok("Senha alterada com sucesso!");
        }
    }
}
