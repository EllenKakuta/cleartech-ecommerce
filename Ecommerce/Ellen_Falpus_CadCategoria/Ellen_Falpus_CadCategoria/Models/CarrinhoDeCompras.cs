using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ellen_Falpus_CadCategoria.Models
{
    public class CarrinhoDeCompras
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public int Numero { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public int QuantidadeTotalProdutos { get; set; }
        public decimal ValorTotal { get; set; }

        [JsonIgnore]
        public virtual List<ProdutoDoCarrinho>ProdutosCarrinho { get; set; }
    }
}
