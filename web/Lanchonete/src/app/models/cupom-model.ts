export interface Cupom {
  id: number;
  codigo: string;
  descricao?: string;
  tipoDesconto: string;
  valorDesconto: number;
  valorMinimoPedido?: number;
  limiteUsos?: number;
  usosAtuais: number;
  validoAte?: string;
  ativo: boolean;
}

export interface CreateCupomRequest {
  codigo: string;
  descricao?: string;
  tipoDesconto: string;
  valorDesconto: number;
  valorMinimoPedido?: number;
  limiteUsos?: number;
  validoAte?: string;
}

export interface ValidarCupomRequest {
  codigo: string;
  valorPedido: number;
}

export interface ValidarCupomResponse {
  valido: boolean;
  mensagem?: string;
  desconto: number;
}