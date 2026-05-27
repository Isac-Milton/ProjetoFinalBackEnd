export interface Categoria {
  id: number;
  nome: string;
  descricao?: string;
  ativo: boolean;
  totalProdutos: number;
}

export interface CreateCategoriaRequest {
  nome: string;
  descricao?: string;
}

export interface UpdateCategoriaRequest {
  nome: string;
  descricao?: string;
  ativo: boolean;
}