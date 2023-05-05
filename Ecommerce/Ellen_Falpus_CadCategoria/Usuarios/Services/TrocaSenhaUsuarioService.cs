using FluentResults;
using Microsoft.AspNetCore.Identity;
using Usuarios.Data.Requests;
using Usuarios.Models;

namespace Usuarios.Services
{
    public class TrocaSenhaUsuarioService
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;

        public TrocaSenhaUsuarioService(SignInManager<CustomIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result TrocaSenha (TrocaSenhaUsuarioRequest request)
        {
            var usuario = _signInManager.UserManager.FindByEmailAsync(request.Email).Result;
            var trocasenha = _signInManager.UserManager.ChangePasswordAsync(usuario, request.Password, request.NewPassword).Result;

            if(trocasenha.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Favor verificar dados inseridos");
        }
    }
}
