export interface PedidoRecente {
  id: number;
  nomeCliente?: string;
  total: number;
  status: string;
  criadoEm: string;
}

export interface ProdutoMaisVendido {
  nome: string;
  quantidade: number;
  receita: number;
}

export interface FaturamentoDiario {
  data: string;
  valor: number;
  pedidos: number;
}

export interface Dashboard {
  totalPedidosHoje: number;
  faturamentoHoje: number;
  pedidosEmAberto: number;
  produtosEstoqueBaixo: number;
  pedidosRecentes: PedidoRecente[];
  produtosMaisVendidos: ProdutoMaisVendido[];
  faturamentoSemana: FaturamentoDiario[];
}