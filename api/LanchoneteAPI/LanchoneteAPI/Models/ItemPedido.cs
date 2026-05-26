using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchoneteAPI.Models;

public class ItemPedido
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Pedido))]
    public int PedidoId { get; set; }
    public Pedido? Pedido { get; set; }

    [ForeignKey(nameof(Produto))]
    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }

    public int Quantidade { get; set; }

    [Column(TypeName = "decimal(10,2)")] public decimal PrecoUnitario { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal Subtotal { get; set; }

    [MaxLength(300)]
    public string? Observacao { get; set; }
}