using AutoMapper;
using Dapper;
using Ellen_Falpus_CadCategoria.Data;
using Ellen_Falpus_CadCategoria.Data.Dtos.CentroDto;
using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ellen_Falpus_CadCategoria.Repository
{
    public class CentroRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;   


        public CentroRepository(EcommerceDbContext context, IDbConnection dbConnection, IMapper mapper)
        {
            _context = context;
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

        public void AdicionaCentro( CentroDeDistribuicao centro)
        {

            _context.Add(centro);
            _context.SaveChanges();
        }

        public CentroDeDistribuicao RecuperarCentroPorId(int id)
        {
            CentroDeDistribuicao centro = _context.CentrosDeDistribuicao.FirstOrDefault(centro => centro.Id == id);
            return centro;
        }

        //public CentroDeDistribuicao RecuperaCentro()
        //{
        //    CentroDeDistribuicao centro = _context.CentrosDeDistribuicao.All();
        //        return centro;
        //}

        public List<Produto> BuscaProdutoPorId(int id)
        {
            List<Produto> produtos = _context.Produtos.Where(produto => produto.CentroId == id && produto.Status == true).ToList();
            return produtos;
        }



        public CentroDeDistribuicao NomeDocentro(CreateCentroDto centroDto)
        {
            CentroDeDistribuicao nome = _context.CentrosDeDistribuicao.FirstOrDefault(centro => centro.Nome.ToLower() == centroDto.Nome.ToLower());
            return nome;
        }

        public void EditarCentro(CentroDeDistribuicao centro)
        {
            _context.Update(centro);
            _context.SaveChanges();
        }

        public Result ApagaCentro(CentroDeDistribuicao centro)
        {
            _context.Remove(centro);
            _context.SaveChanges();
            return Result.Ok();
        }

        public CentroDeDistribuicao Endereco(CreateCentroDto centroDto)
        {
            var endereco = _context.CentrosDeDistribuicao.FirstOrDefault(centro => centro.CEP == centroDto.CEP);
            return endereco;
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }


        public List<CentroDeDistribuicao> FiltraCentro(string nome, bool? status, string cep, string logradouro, int? numero, string uf,
            string bairro, string localidade, string complemento, string ordem, int qtde, int pagina)
        {
            var consulta = "SELECT * FROM centrosdedistribuicao WHERE ";
          
            if (nome != null)
            {
                consulta += "Nome LIKE '%" + nome + "%' AND"; 
            }
            if (status != null)
            {
                consulta += "Status = @status AND"; 
            }
            if (cep != null)
            {
                consulta += "CEP LIKE '%" + cep + "%' AND";
            }
            if (logradouro != null)
            {
                consulta += "Logradouro LIKE '%" + logradouro + "%' AND";
            }
            if (bairro != null)
            {
                consulta += "Bairro LIKE '%" + bairro + "%' AND";
            }
            if (numero != null)
            {
                consulta += "Numero = @numero AND";
            }
            if (uf != null)
            {
                consulta += "UF= @uf AND";
            }
            if (localidade != null)
            {
                consulta += "Localidade LIKE '%" + localidade + "%' AND";
            }
            if (complemento != null)
            {
                consulta += "Complemento  LIKE '%" + complemento + "%' AND";
            }
            if (nome == null && cep == null && status == null && nome == null && status == null && logradouro == null && numero == null
                && uf == null && localidade == null)
            {
                var deleteWhere = consulta.LastIndexOf("WHERE");
                consulta = consulta.Remove(deleteWhere);
            }
            else
            {
                var deleteAnd = consulta.LastIndexOf("AND");
                consulta = consulta.Remove(deleteAnd);
            }
            if (ordem != null)
            {
                if (ordem.ToLower() == "up")
                {
                    consulta += "ORDER BY nome";
                }
                if (ordem.ToLower() == "down")
                {
                    consulta += "ORDER BY nome DESC";
                }
               
            }
            //Console.WriteLine(consulta);
            var result = _dbConnection.Query<CentroDeDistribuicao>(consulta, new
            {
                Nome = nome,
                Status = status,
                Logradouro = logradouro,
                Numero = numero,
                UF = uf,
                Localidade = localidade,
                Bairro = bairro,
                CEP = cep,
                Complemento = complemento
            });

            if (qtde > 0 && pagina > 0)
            {
                var resultado = result.Skip((pagina - 1) * qtde).Take(qtde).ToList();
                return resultado;
               
            }
            return result.ToList();
         
        }

    }

   
}
