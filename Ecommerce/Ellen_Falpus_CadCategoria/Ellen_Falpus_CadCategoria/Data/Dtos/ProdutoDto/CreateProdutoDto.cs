using System;
using System.ComponentModel.DataAnnotations;

namespace Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDto
{
    public class CreateProdutoDto
    {
        [Required(ErrorMessage ="O nome do produto é obrigatório")]
        [StringLength(128, MinimumLength =3,ErrorMessage = "Permitido o uso de no mínimo 3 e no máximo 128 caracteres alfanuméricos")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="O campo descrição é obrigatório")]
        [StringLength(512, MinimumLength =3,ErrorMessage = "Permitido o uso de no mínimo 3 e no máximo 512 caracteres alfanuméricos")]
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
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; }= DateTime.Now;
        [Required(ErrorMessage = "ID da subcategoria obrigatório")]
        public int SubcategoriaId { get; set; }
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "ID centro de distribuição obrigatório.")]
        public int CentroId { get; set; }

    }
}
