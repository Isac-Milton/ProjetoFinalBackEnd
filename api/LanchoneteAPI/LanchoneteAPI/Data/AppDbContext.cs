using Microsoft.EntityFrameworkCore;
using LanchoneteAPI.Models;

namespace LanchoneteAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cupom> Cupons { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categoria>(e =>
        {
            e.ToTable("Categorias");
            e.HasIndex(c => c.Nome).IsUnique();
        });

        modelBuilder.Entity<Produto>(e =>
        {
            e.ToTable("Produtos");
            e.HasOne(p => p.Categoria)
             .WithMany(c => c.Produtos)
             .HasForeignKey(p => p.CategoriaId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("Usuarios");
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Perfil).HasConversion<string>();
        });

        modelBuilder.Entity<Cupom>(e =>
        {
            e.ToTable("Cupons");
            e.HasIndex(c => c.Codigo).IsUnique();
            e.Property(c => c.TipoDesconto).HasConversion<string>();
        });

        modelBuilder.Entity<Pedido>(e =>
        {
            e.ToTable("Pedidos");
            e.Property(p => p.Status).HasConversion<string>();
            e.Property(p => p.Tipo).HasConversion<string>();
            e.Property(p => p.FormaPagamento).HasConversion<string>();

            e.HasOne(p => p.Usuario)
             .WithMany(u => u.Pedidos)
             .HasForeignKey(p => p.UsuarioId)
             .OnDelete(DeleteBehavior.SetNull);

            e.HasOne(p => p.Cupom)
             .WithMany(c => c.Pedidos)
             .HasForeignKey(p => p.CupomId)
             .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<ItemPedido>(e =>
        {
            e.ToTable("ItensPedido");

            e.HasOne(i => i.Pedido)
             .WithMany(p => p.Itens)
             .HasForeignKey(i => i.PedidoId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(i => i.Produto)
             .WithMany(p => p.ItensPedido)
             .HasForeignKey(i => i.ProdutoId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}