using System.ComponentModel.DataAnnotations;

namespace LanchoneteAPI.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Descricao { get; set; }

    public bool Ativo { get; set; } = true;

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    public ICollection<Produto> Produtos { get; set; } = [];
}