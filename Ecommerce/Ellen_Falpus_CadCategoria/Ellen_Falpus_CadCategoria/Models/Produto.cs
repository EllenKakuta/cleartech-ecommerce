using Ellen_Falpus_CadCategoria.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ellen_Falpus_CadCategoria.Modelos
{
    public class Produto
    {
        [Key]
        [Required]  
        public int Id { get; set; }

        [Required(ErrorMessage ="O nome do produto é obrigatório")]
        [StringLength(128, MinimumLength =3,ErrorMessage = "Permitido o uso de no mínimo 3 e no máximo 128 caracteres alfanuméricos")]
 
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo descrição campo é obrigatório")]
        [StringLength(512,MinimumLength =3, ErrorMessage = "Permitido o uso de no mínimo 3 e no máximo 512 caracteres alfanuméricos")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O campo peso é obrigatório")]
        public decimal Peso { get; set; }
        [Required(ErrorMessage = "O campo altura é obrigatório")]
        public decimal Altura { get; set; }
        [Required(ErrorMessage = "O campo largura é obrigatório")]
        public decimal Largura { get; set; }
        [Required(ErrorMessage = "O campo comprimento é obrigatório")]
        public decimal Comprimento { get; set; }
        [Required(ErrorMessage = "O campo valor é obrigatório")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "O campo estoque é obrigatório")]
        public int Estoque { get; set; }
        //public string CentroDistribuicao { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public virtual Subcategoria Subcategoria { get; set; }
        public int SubcategoriaId { get; set; }
        [JsonIgnore]
        public virtual Categoria Categoria { get; set; }
        [JsonIgnore]
        public int CategoriaId { get; set; }   

        public virtual CentroDeDistribuicao CentroDeDistribuicao { get; set; }
        [Required(ErrorMessage ="O ID do centro de distribuição é obrigatório")]
        public int CentroId { get; set; }


    }
}
