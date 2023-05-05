using FluentResults;
using Microsoft.AspNetCore.Identity;
using Usuarios.Data.Requests;
using Usuarios.Models;

namespace Usuarios.Services
{

    public class LogoutUsuarioService
    {
        private SignInManager<CustomIdentityUser> _signInManager;

        public LogoutUsuarioService(SignInManager<CustomIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result DeslogaUsuario(LogoutUsuarioRequest request)
        {
            var usuario = _signInManager.UserManager.FindByEmailAsync(request.Email);
            var resultadoIdentity = _signInManager.SignOutAsync();
            if (resultadoIdentity.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("Falha no logout");
        }
    }
}
