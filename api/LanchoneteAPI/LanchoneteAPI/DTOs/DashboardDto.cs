namespace LanchoneteAPI.DTOs;

public record DashboardDto(
    int TotalPedidosHoje, decimal FaturamentoHoje,
    int PedidosEmAberto, int ProdutosEstoqueBaixo,
    List<PedidoRecenteDto> PedidosRecentes,
    List<ProdutoMaisVendidoDto> ProdutosMaisVendidos,
    List<FaturamentoDiarioDto> FaturamentoSemana
);
public record PedidoRecenteDto(int Id, string? NomeCliente, decimal Total, string Status, DateTime CriadoEm);
public record ProdutoMaisVendidoDto(string Nome, int Quantidade, decimal Receita);
public record FaturamentoDiarioDto(string Data, decimal Valor, int Pedidos);