namespace LanchoneteAPI.DTOs;

public record ItemPedidoDto(int Id, int ProdutoId, string ProdutoNome, int Quantidade, decimal PrecoUnitario, decimal Subtotal, string? Observacao);

public record PedidoDto(
    int Id, int? UsuarioId, string? UsuarioNome,
    string Status, string Tipo, string FormaPagamento,
    decimal Subtotal, decimal Desconto, decimal TaxaEntrega, decimal Total,
    string? CupomCodigo, string? NomeCliente, string? TelefoneCliente,
    string? EnderecoEntrega, string? Observacao,
    DateTime CriadoEm, DateTime? FinalizadoEm,
    List<ItemPedidoDto> Itens
);
public record CreateItemRequest(int ProdutoId, int Quantidade, string? Observacao);
public record CreatePedidoRequest(
    int? UsuarioId, string Tipo, string FormaPagamento,
    string? CupomCodigo, string? NomeCliente, string? TelefoneCliente,
    string? EnderecoEntrega, string? Observacao,
    List<CreateItemRequest> Itens
);
public record AtualizarStatusRequest(string Status);