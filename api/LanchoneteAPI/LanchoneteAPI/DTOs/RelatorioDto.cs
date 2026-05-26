namespace LanchoneteAPI.DTOs;

public record RelatorioVendasRequest(DateTime DataInicio, DateTime DataFim);
public record RelatorioVendasDto(
    decimal TotalFaturado, int TotalPedidos, decimal TicketMedio,
    List<FaturamentoDiarioDto> PorDia,
    List<ProdutoMaisVendidoDto> ProdutosMaisVendidos,
    Dictionary<string, int> PedidosPorStatus,
    Dictionary<string, decimal> FaturamentoPorCategoria
);