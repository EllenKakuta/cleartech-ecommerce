using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ellen_Falpus_CadCategoria.Modelos
{
    public class Categoria
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Permitido mínimo de 1 e máximo de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú çÇ''\s]{1,50}$", ErrorMessage = "Permitido somente o uso de letras")]
        public string Nome { get; set; }

        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } 
        public DateTime DataAlteracao { get; set; }
        [JsonIgnore]
        public virtual List<Subcategoria>Subcategorias { get; set; }
        [JsonIgnore]
        public virtual List<Produto> Produtos { get; set; }
    }
}
