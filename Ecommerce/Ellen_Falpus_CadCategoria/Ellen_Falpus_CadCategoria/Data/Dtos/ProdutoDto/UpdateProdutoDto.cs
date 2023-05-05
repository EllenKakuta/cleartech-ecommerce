using System;
using System.ComponentModel.DataAnnotations;

namespace Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDto
{
    public class UpdateProdutoDto
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        [StringLength(128,MinimumLength =3, ErrorMessage = "Permitido o uso de no mínimo 3 e no máximo 128 caracteres alfanuméricos")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(512, MinimumLength =3, ErrorMessage = "Permitido o uso de no mínimo 3 e no máximo 512 caracteres alfanuméricos")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public decimal Peso { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public decimal Altura { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public decimal Largura { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public decimal Comprimento { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int Estoque { get; set; }
        public bool Status { get; set; }
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}
