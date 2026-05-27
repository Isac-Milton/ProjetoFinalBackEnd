import { FaturamentoDiario, ProdutoMaisVendido } from './dashboard-model';

export interface RelatorioVendasRequest {
  dataInicio: string;
  dataFim: string;
}

export interface RelatorioVendas {
  totalFaturado: number;
  totalPedidos: number;
  ticketMedio: number;
  porDia: FaturamentoDiario[];
  produtosMaisVendidos: ProdutoMaisVendido[];
  pedidosPorStatus: Record<string, number>;
  faturamentoPorCategoria: Record<string, number>;
}