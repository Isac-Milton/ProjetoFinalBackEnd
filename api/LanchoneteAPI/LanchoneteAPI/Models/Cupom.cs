using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchoneteAPI.Models;

public class Cupom
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Codigo { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Descricao { get; set; }

    public TipoDesconto TipoDesconto { get; set; } = TipoDesconto.Percentual;

    [Column(TypeName = "decimal(10,2)")]
    public decimal ValorDesconto { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? ValorMinimoPedido { get; set; }

    public int? LimiteUsos { get; set; }
    public int UsosAtuais { get; set; } = 0;

    public DateTime? ValidoAte { get; set; }

    public bool Ativo { get; set; } = true;

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    public ICollection<Pedido> Pedidos { get; set; } = [];
}