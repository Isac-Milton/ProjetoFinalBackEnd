import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth-service';
import { Produto, CreateProdutoRequest, UpdateProdutoRequest, AtualizarEstoqueRequest } from '../models/produto-model';
import { retry } from 'rxjs';

@Injectable({ providedIn: 'root' })

export class ProdutoService {

  private readonly API = 'https://projetofinalbackend-production.up.railway.app/api';
  constructor(private http: HttpClient, private auth: AuthService) {}

  getAll() { return this.http.get<Produto[]>(`${this.API}/produtos`, { headers: this.auth.getHeaders() }).pipe(retry(1)); }
  
  getById(id: number) { return this.http.get<Produto>(`${this.API}/produtos/${id}`, { headers: this.auth.getHeaders() }); }
  
  getEstoqueBaixo() { return this.http.get<Produto[]>(`${this.API}/produtos/estoque-baixo`, { headers: this.auth.getHeaders() }); }
  
  create(data: CreateProdutoRequest) { return this.http.post<Produto>(`${this.API}/produtos`, data, { headers: this.auth.getHeaders() }); }
  
  update(id: number, data: UpdateProdutoRequest) { return this.http.put<Produto>(`${this.API}/produtos/${id}`, data, { headers: this.auth.getHeaders() }); }
  
  atualizarEstoque(id: number, data: AtualizarEstoqueRequest) { return this.http.patch<Produto>(`${this.API}/produtos/${id}/estoque`, data, { headers: this.auth.getHeaders() }); }
  
  delete(id: number) { return this.http.delete(`${this.API}/produtos/${id}`, { headers: this.auth.getHeaders() }); }
}
