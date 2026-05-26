using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchoneteAPI.Models;

public class Pedido
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Usuario))]
    public int? UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    public StatusPedido Status { get; set; } = StatusPedido.Pendente;
    public TipoPedido Tipo { get; set; } = TipoPedido.Local;
    public FormaPagamento FormaPagamento { get; set; } = FormaPagamento.Dinheiro;

    [Column(TypeName = "decimal(10,2)")] public decimal Subtotal { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal Desconto { get; set; } = 0;
    [Column(TypeName = "decimal(10,2)")] public decimal TaxaEntrega { get; set; } = 0;
    [Column(TypeName = "decimal(10,2)")] public decimal Total { get; set; }

    [ForeignKey(nameof(Cupom))]
    public int? CupomId { get; set; }
    public Cupom? Cupom { get; set; }

    [MaxLength(100)] public string? NomeCliente { get; set; }
    [MaxLength(20)] public string? TelefoneCliente { get; set; }
    [MaxLength(300)] public string? EnderecoEntrega { get; set; }
    [MaxLength(500)] public string? Observacao { get; set; }

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? FinalizadoEm { get; set; }

    public ICollection<ItemPedido> Itens { get; set; } = [];
}