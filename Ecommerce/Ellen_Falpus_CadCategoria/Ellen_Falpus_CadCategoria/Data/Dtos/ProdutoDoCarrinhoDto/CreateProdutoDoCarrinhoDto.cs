using System.ComponentModel.DataAnnotations;

namespace Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDoCarrinhoDto
{
    public class CreateProdutoDoCarrinhoDto
    {
        [Required]
        public int IdCarrinho { get; set; }
        [Required]
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorUnitario { get; set; }
        [Required]
        public int QuantidadeProduto { get; set; }
    }
}
