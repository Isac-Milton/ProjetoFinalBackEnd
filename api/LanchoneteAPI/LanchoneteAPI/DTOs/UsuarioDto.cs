namespace LanchoneteAPI.DTOs;

public record UsuarioDto(int Id, string Nome, string Email, string Perfil, string? Telefone, string? Endereco, bool Ativo, DateTime CriadoEm);
public record CreateUsuarioRequest(string Nome, string Email, string Senha, string Perfil, string? Telefone, string? Endereco);
public record UpdateUsuarioRequest(string Nome, string? Telefone, string? Endereco, bool Ativo);