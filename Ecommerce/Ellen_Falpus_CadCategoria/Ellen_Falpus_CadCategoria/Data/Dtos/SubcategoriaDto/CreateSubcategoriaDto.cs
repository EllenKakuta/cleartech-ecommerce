using System;
using System.ComponentModel.DataAnnotations;

namespace Ellen_Falpus_CadCategoria.Data.Dtos.SubcategoriaDto
{
    public class CreateSubcategoriaDto
    {
        [Required(ErrorMessage = "O nome da subcategoria é obrigatório")]
        [StringLength(50,MinimumLength =3, ErrorMessage = "Permitido mínimo de 3 e máximo de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú çÇ''\s]{1,50}$", ErrorMessage = "Permitido somente o uso de letras")]
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public int CategoriaId  { get; set; } 
    }
}
