namespace LanchoneteAPI.DTOs;

public record ProdutoDto(
    int Id, string Nome, string? Descricao, decimal Preco,
    int CategoriaId, string CategoriaNome,
    int EstoqueQuantidade, int EstoqueMinimo,
    bool Disponivel, bool Ativo, bool EstoqueBaixo
);
public record CreateProdutoRequest(string Nome, string? Descricao, decimal Preco, int CategoriaId, int EstoqueQuantidade, int EstoqueMinimo);
public record UpdateProdutoRequest(string Nome, string? Descricao, decimal Preco, int CategoriaId, int EstoqueQuantidade, int EstoqueMinimo, bool Disponivel, bool Ativo);
public record AtualizarEstoqueRequest(int Quantidade, string Operacao);