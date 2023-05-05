using Ellen_Falpus_CadCategoria.Data.Dtos.CarrinhoDeComprasDto;
using Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDoCarrinhoDto;
using Ellen_Falpus_CadCategoria.Repository;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ellen_Falpus_CadCategoria.Models
{
   
    public class ProdutoDoCarrinho
    {   

        [Key]
        public int Id { get; set; }
        public int IdCarrinho { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorUnitario { get; set; }
        public int QuantidadeProduto { get; set; }
        public virtual CarrinhoDeCompras Carrinho { get; set; }


       
    }


    

  

}
