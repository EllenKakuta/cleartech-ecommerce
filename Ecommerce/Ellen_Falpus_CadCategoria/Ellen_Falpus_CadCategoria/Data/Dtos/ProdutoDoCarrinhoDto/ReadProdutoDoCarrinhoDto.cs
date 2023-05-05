namespace Ellen_Falpus_CadCategoria.Data.Dtos.ProdutoDoCarrinhoDto
{
    public class ReadProdutoDoCarrinhoDto
    {
        public int Id { get; set; }
        public int IdCarrinho { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorUnitario { get; set; }
        public int QuantidadeProduto { get; set; }
    }
}
