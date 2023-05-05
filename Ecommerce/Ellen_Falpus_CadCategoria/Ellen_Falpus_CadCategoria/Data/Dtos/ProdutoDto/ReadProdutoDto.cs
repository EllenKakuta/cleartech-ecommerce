using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDto
{
    public class ReadProdutoDto
    {
       
        public int Id { get; set; }     
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }
        public decimal Valor { get; set; }
        public int Estoque { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } 
        public DateTime DataAlteracao { get; set; }
        public Subcategoria Subcategoria { get; set; }
        [JsonIgnore]
        public Categoria Categoria { get; set; }
        public CentroDeDistribuicao CentroDeDistribuicao { get; set; }
    }
}
