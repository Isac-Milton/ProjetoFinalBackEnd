namespace LanchoneteAPI.DTOs;

public record CupomDto(int Id, string Codigo, string? Descricao, string TipoDesconto, decimal ValorDesconto, decimal? ValorMinimoPedido, int? LimiteUsos, int UsosAtuais, DateTime? ValidoAte, bool Ativo);
public record CreateCupomRequest(string Codigo, string? Descricao, string TipoDesconto, decimal ValorDesconto, decimal? ValorMinimoPedido, int? LimiteUsos, DateTime? ValidoAte);
public record ValidarCupomRequest(string Codigo, decimal ValorPedido);
public record ValidarCupomResponse(bool Valido, string? Mensagem, decimal Desconto);