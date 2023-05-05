using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using Usuarios.Data.Requests;
using Usuarios.Models;

namespace Usuarios.Services
{
    public class LoginUsuarioService
    {
        private SignInManager<CustomIdentityUser> _signInManager;
        private TokenUsuarioService _tokenService;

        public LoginUsuarioService(SignInManager<CustomIdentityUser> signInManager, TokenUsuarioService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginUsuarioRequest request)
        {
            var usuario = _signInManager.UserManager.FindByEmailAsync(request.Email);
            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(usuario.Result.UserName, request.Password, false, false);
            if (resultadoIdentity.Result.Succeeded) 
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(u => u.NormalizedUserName == usuario.Result.UserName.ToUpper());
                TokenUsuario token = _tokenService
                    .CreateToken(identityUser, _signInManager.UserManager
                    .GetRolesAsync(identityUser).Result.FirstOrDefault());
                return Result.Ok().WithSuccess(token.Value);    
            }
            return Result.Fail("Falha no login, verifique os dados inseridos");
        }
    }
}
