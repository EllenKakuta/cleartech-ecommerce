using System;
using System.ComponentModel.DataAnnotations;

namespace Ellen_Falpus_CadCategoria.Data.Dtos.CategoriaDto
{
    public class UpdateCategoriaDto
    {

        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Permitido mínimo de 3 e máximo de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú çÇ''\s]{1,50}$", ErrorMessage = "Permitido somente o uso de letras")]
        public string Nome { get; set; }

        public bool Status { get; set; }
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}
