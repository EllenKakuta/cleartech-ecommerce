using Ellen_Falpus_CadCategoria.Modelos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ellen_Falpus_CadCategoria.Data.Dtos.SubcategoriaDto
{
    public class ReadSubcategoriaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public Categoria Categoria { get; set; }
        public object Produtos { get; set; }    
    }
}
