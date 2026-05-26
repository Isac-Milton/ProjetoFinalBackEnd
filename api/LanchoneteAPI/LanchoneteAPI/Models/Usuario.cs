using System.ComponentModel.DataAnnotations;

namespace LanchoneteAPI.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string SenhaHash { get; set; } = string.Empty;

    public PerfilUsuario Perfil { get; set; } = PerfilUsuario.Cliente;

    [MaxLength(20)]
    public string? Telefone { get; set; }

    [MaxLength(300)]
    public string? Endereco { get; set; }

    public bool Ativo { get; set; } = true;

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    public ICollection<Pedido> Pedidos { get; set; } = [];
}