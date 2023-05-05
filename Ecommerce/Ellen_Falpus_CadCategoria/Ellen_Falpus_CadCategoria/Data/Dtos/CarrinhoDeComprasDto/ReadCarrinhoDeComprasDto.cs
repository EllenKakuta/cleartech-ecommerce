using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Ellen_Falpus_CadCategoria.Data.Dtos.CarrinhoDeComprasDto
{
    public class ReadCarrinhoDeComprasDto
    {
        public int Id { get; set; }
        public string CEP { get; set; }
        public int Numero { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public int QuantidadeTotalProdutos { get; set; }
        public double ValorTotal { get; set; }
        //public object ProdutosCarrinho { get; set; }



    }
}
