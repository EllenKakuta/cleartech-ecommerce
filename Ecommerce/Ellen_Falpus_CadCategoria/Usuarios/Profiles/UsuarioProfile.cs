using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Usuarios.Data.Dtos.Usuario;
using Usuarios.Models;

namespace Usuarios.Profiles
{
    public class UsuarioProfile : Profile 
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, IdentityUser<int>>();
            CreateMap<Usuario, CustomIdentityUser>();
            CreateMap<Usuario,ReadUsuarioDto>();
            CreateMap< ReadUsuarioDto, Usuario>();
            CreateMap<CustomIdentityUser, ReadUsuarioDto>();
            CreateMap<ReadUsuarioDto,CustomIdentityUser>();
            CreateMap<UpdateUsuarioDto, Usuario>();
            CreateMap<CustomIdentityUser, UpdateUsuarioDto>();
            CreateMap<UpdateUsuarioDto, CustomIdentityUser>();
            CreateMap<CustomIdentityUser, IdentityUser<int>>();
            CreateMap<IdentityUser<int>, CustomIdentityUser>();
        }

    }
}

