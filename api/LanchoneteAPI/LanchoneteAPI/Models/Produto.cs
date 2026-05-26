using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchoneteAPI.Models;

public class Produto
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Descricao { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [ForeignKey(nameof(Categoria))]
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public int EstoqueQuantidade { get; set; } = 0;
    public int EstoqueMinimo { get; set; } = 5;

    public bool Disponivel { get; set; } = true;
    public bool Ativo { get; set; } = true;

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

    public ICollection<ItemPedido> ItensPedido { get; set; } = [];
}