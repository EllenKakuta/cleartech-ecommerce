using Ellen_Falpus_CadCategoria.Modelos;
using Ellen_Falpus_CadCategoria.Models;
using Microsoft.EntityFrameworkCore;

namespace Ellen_Falpus_CadCategoria.Data
{
    public class EcommerceDbContext : DbContext 
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> opt) : base(opt)
        {

        }

        public EcommerceDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Subcategoria>()
                .HasOne(subcategoria=> subcategoria.Categoria)
                .WithMany(categoria=> categoria.Subcategorias)
                .HasForeignKey(subcategoria=>subcategoria.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Produto>()
                .HasOne(produto => produto.Categoria)
                .WithMany(categoria => categoria.Produtos)
                .HasForeignKey(produto => produto.CategoriaId);
               
           
            builder.Entity<Produto>()
              .HasOne(produto => produto.Subcategoria)
              .WithMany(subcategoria => subcategoria.Produtos)
              .HasForeignKey(produto => produto.SubcategoriaId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Produto>()
                .HasOne(produto => produto.CentroDeDistribuicao)
                .WithMany(centroDeDistribuicao => centroDeDistribuicao.Produtos)
                .HasForeignKey(produto => produto.CentroId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProdutoDoCarrinho>()
                .HasOne(prod => prod.Carrinho)
                .WithMany(carrinho => carrinho.ProdutosCarrinho)
                .HasForeignKey(prod => prod.IdCarrinho);
               

        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Subcategoria> Subcategorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CentroDeDistribuicao> CentrosDeDistribuicao { get; set; }
        public DbSet<CarrinhoDeCompras> CarrinhoDeCompras { get; set; }
        public DbSet<ProdutoDoCarrinho> ProdutoDoCarrinho { get; set; }

        
       
    }
}
