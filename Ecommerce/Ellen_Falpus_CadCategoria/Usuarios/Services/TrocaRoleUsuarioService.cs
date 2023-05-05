using FluentResults;
using Microsoft.AspNetCore.Identity;
using Usuarios.Data.Requests;
using Usuarios.Models;

namespace Usuarios.Services
{
    public class TrocaRoleUsuarioService
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;


        public TrocaRoleUsuarioService(SignInManager<CustomIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }


        public Result TrocaRole (TrocaRoleUsuarioRequest request)
        {
            var usuario = _signInManager.UserManager.FindByEmailAsync(request.Email).Result;           
            if(usuario == null)
            {
                return Result.Fail("Usuário inexistente");
            }

            var cliente = _signInManager.UserManager.IsInRoleAsync(usuario, "regular");
            if (cliente.Result== true)
            {
                return Result.Fail("Alteração de permissão não autorizada para esse usuário");
            }

            var trocarole = _signInManager.UserManager.AddToRoleAsync(usuario, request.Role).Result;
            return Result.Ok();

        }

    }
}
