using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Usuarios.Data;
using Usuarios.Data.Dtos.Usuario;
using Usuarios.Data.Requests;
using Usuarios.Models;

namespace Usuarios.Services
{
    public class CadastroUsuarioService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<IdentityRole<int>> _roleManager;

        public CadastroUsuarioService(IMapper mapper, UserManager<CustomIdentityUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<CEPDto> BuscaCep(string cep)
        {

            HttpClient client = new HttpClient();
            var consulta = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");       
            var resultado = await consulta.Content.ReadAsStringAsync();
            var endereco = JsonConvert.DeserializeObject<CEPDto>(resultado);

            if (String.IsNullOrWhiteSpace(endereco.Logradouro))
            {
                throw new ArgumentNullException("Não é possível cadastrar CEP geral devido a não ter logradouro especificado");
            }

            return endereco;
        }


        public static bool ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public bool DataDeNascimento(CreateUsuarioDto createDto)
        {
            if (createDto.DataNascimento> DateTime.Today)
            {
                return true;
            }
            return false;
        }

        public bool ValidaEmail ( CreateUsuarioDto dto)
        {
            var _email = _userManager.Users.FirstOrDefault(u => u.Email == dto.Email);
              
             if (_email == null)
             {
                 return true;
             }
             return false;
            
        }

        public bool ValidaUsername (CreateUsuarioDto dto)
        {
            var username = _userManager.Users.FirstOrDefault(u => u.UserName== dto.Username);
            if(username == null)
            {
                return true;
            }
            return false;   
        }

        public bool ValidaCPFCadastrado (CreateUsuarioDto dto)
        {
            var cpf = _userManager.Users.FirstOrDefault(u=> u.CPF== dto.CPF);
            if(cpf == null)
            {
                return true;
            }
            return false;
        }
       
           
        public async Task<Result> CadastraUsuario(CreateUsuarioDto createDto)
        {

            var endereco = await BuscaCep(createDto.CEP);
            if (endereco == null)
            {
                return Result.Fail("Digite um CEP válido");
            }
                 
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            usuario.Logradouro = endereco.Logradouro;
            usuario.Bairro = endereco.Bairro;
            usuario.Localidade = endereco.Localidade;
            usuario.UF = endereco.UF;

            if (ValidaCPF(createDto.CPF) == false)
            {
                return Result.Fail("Digite um CPF válido");
            }

            if (DataDeNascimento(createDto) != false)
            {
                return Result.Fail("Data de nascimento inválida");
            }

            var email = ValidaEmail(createDto);
            if (email == false)
            {
                return Result.Fail("E-mail já cadastrado");
            }

            var cpf = ValidaCPFCadastrado(createDto);
            if (cpf == false)
            {
                return Result.Fail("CPF já cadastrado");
            }

            var username = ValidaUsername(createDto);
            if (username == false)
            {
                return Result.Fail("Nome de usuário já cadastrado");
            }


            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);
            var resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, createDto.Password);
            var usuarioUserRole = await _userManager.AddToRoleAsync(usuarioIdentity, "regular");


            if (resultadoIdentity.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                return Result.Ok().WithSuccess(code);
            }
            return Result.Fail("Falha ao cadastrar usuário");
        }

        public async Task<List<ReadUsuarioDto>> ListaUsuario (bool? status, string username, string cpf, string email)
        {
            var usuario = await _userManager.Users.ToListAsync();
            List <ReadUsuarioDto> listaUsuario = new();

            foreach (var u in usuario)
            {
                var lista = _mapper.Map<ReadUsuarioDto>(u);
                listaUsuario.Add(lista);
            }
            if (username != null)
            {
                return listaUsuario.Where(user => user.UserName.ToLower().Equals(username.ToLower())).ToList();
            }
            if (cpf != null)
            {
                return listaUsuario.Where(user => user.CPF == cpf).ToList();
            }
            if (email != null)
            {
                return listaUsuario.Where(user => user.Email == email).ToList();
            }
            if (status != null)
            {
                return listaUsuario.Where(user => user.Status == status).ToList();
            }
            return listaUsuario;

        }


        public  Result AtivaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager.Users.FirstOrDefault(u=> u.Id == request.UsuarioId);
            var identityResult = _userManager.ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao).Result;
            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao ativar conta de usuário");
        }

        public async Task<Result> EditaUsuario (int id, UpdateUsuarioDto dto)
        {
            var usuario = _userManager.Users.FirstOrDefault(u => u.Id == id);
            usuario.DataModificacao = DateTime.Now;
            if(usuario == null)
            {
                throw new ArgumentException();
            }
                    
            var endereco = await BuscaCep(dto.CEP);
            dto.Logradouro = endereco.Logradouro;
            dto.Bairro = endereco.Bairro;
            dto.Localidade = endereco.Localidade;
            dto.UF = endereco.UF;
           
            var map = _mapper.Map(dto, usuario);
            await _userManager.UpdateAsync(usuario);
            return Result.Ok();
            
        }


        public async Task<Result> CadastraLojista(CreateUsuarioDto createDto)
        {

            var endereco = await BuscaCep(createDto.CEP);
            if (endereco == null)
            {
                return Result.Fail("Digite um CEP válido");
            }

            Usuario usuario = _mapper.Map<Usuario>(createDto);
            usuario.Logradouro = endereco.Logradouro;
            usuario.Bairro = endereco.Bairro;
            usuario.Localidade = endereco.Localidade;
            usuario.UF = endereco.UF;

            if (ValidaCPF(createDto.CPF) == false)
            {
                return Result.Fail("Digite um CPF válido");
            }

            if (DataDeNascimento(createDto) != false)
            {
                return Result.Fail("Data de nascimento inválida");
            }

            var email = ValidaEmail(createDto);
            if (email == false)
            {
                return Result.Fail("E-mail já cadastrado");
            }

            var cpf = ValidaCPFCadastrado(createDto);
            if (cpf == false)
            {
                return Result.Fail("CPF já cadastrado");
            }
            var username = ValidaUsername(createDto);
            if (username == false)
            {
                return Result.Fail("Nome de usuário já cadastrado");
            }


            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);
            var resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, createDto.Password);
            var usuarioUserRole = await _userManager.AddToRoleAsync(usuarioIdentity, "lojista");
            var createRoleResult = await _roleManager.CreateAsync(new IdentityRole<int>("lojista"));

            if (resultadoIdentity.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                return Result.Ok().WithSuccess(code);
            }
            return Result.Fail("Falha ao cadastrar usuário");
        }



    }
}
    