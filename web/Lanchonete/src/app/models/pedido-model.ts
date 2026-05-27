export interface ItemPedido {
  id: number;
  produtoId: number;
  produtoNome: string;
  quantidade: number;
  precoUnitario: number;
  subtotal: number;
  observacao?: string;
}

export interface Pedido {
  id: number;
  usuarioId?: number;
  usuarioNome?: string;
  status: string;
  tipo: string;
  formaPagamento: string;
  subtotal: number;
  desconto: number;
  taxaEntrega: number;
  total: number;
  cupomCodigo?: string;
  nomeCliente?: string;
  telefoneCliente?: string;
  enderecoEntrega?: string;
  observacao?: string;
  criadoEm: string;
  finalizadoEm?: string;
  itens: ItemPedido[];
}

export interface CreateItemRequest {
  produtoId: number;
  quantidade: number;
  observacao?: string;
}

export interface CreatePedidoRequest {
  usuarioId?: number;
  tipo: string;
  formaPagamento: string;
  cupomCodigo?: string;
  nomeCliente?: string;
  telefoneCliente?: string;
  enderecoEntrega?: string;
  observacao?: string;
  itens: CreateItemRequest[];
}

export interface AtualizarStatusRequest {
  status: string;
}