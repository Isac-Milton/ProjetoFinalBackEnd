export interface Produto {
  id: number;
  nome: string;
  descricao?: string;
  preco: number;
  categoriaId: number;
  categoriaNome: string;
  estoqueQuantidade: number;
  estoqueMinimo: number;
  disponivel: boolean;
  ativo: boolean;
  estoqueBaixo: boolean;
}

export interface CreateProdutoRequest {
  nome: string;
  descricao?: string;
  preco: number;
  categoriaId: number;
  estoqueQuantidade: number;
  estoqueMinimo: number;
}

export interface UpdateProdutoRequest {
  nome: string;
  descricao?: string;
  preco: number;
  categoriaId: number;
  estoqueQuantidade: number;
  estoqueMinimo: number;
  disponivel: boolean;
  ativo: boolean;
}

export interface AtualizarEstoqueRequest {
  quantidade: number;
  operacao: string;
}