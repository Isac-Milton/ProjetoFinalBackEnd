namespace LanchoneteAPI.DTOs;

public record CategoriaDto(int Id, string Nome, string? Descricao, bool Ativo, int TotalProdutos);
public record CreateCategoriaRequest(string Nome, string? Descricao);
public record UpdateCategoriaRequest(string Nome, string? Descricao, bool Ativo);