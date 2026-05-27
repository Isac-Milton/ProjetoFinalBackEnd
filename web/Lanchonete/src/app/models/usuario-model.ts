export interface Usuario {
  id: number;
  nome: string;
  email: string;
  perfil: string;
  telefone?: string;
  endereco?: string;
  ativo: boolean;
  criadoEm: string;
}

export interface CreateUsuarioRequest {
  nome: string;
  email: string;
  senha: string;
  perfil: string;
  telefone?: string;
  endereco?: string;
}

export interface UpdateUsuarioRequest {
  nome: string;
  telefone?: string;
  endereco?: string;
  ativo: boolean;
}