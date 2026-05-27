import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AuthService } from './auth-service';
import { Pedido, CreatePedidoRequest, AtualizarStatusRequest } from '../models/pedido-model';

@Injectable({ providedIn: 'root' })

export class PedidoService {

  private readonly API = 'http://localhost:5007/api';

  constructor(private http: HttpClient, private auth: AuthService) {}

  getAll(dataInicio?: string, dataFim?: string, status?: string) {
    let params = new HttpParams();
    
    if (dataInicio) params = params.set('dataInicio', dataInicio);
    
    if (dataFim) params = params.set('dataFim', dataFim);
    
    if (status) params = params.set('status', status);
    
    return this.http.get<Pedido[]>(`${this.API}/pedidos`, { headers: this.auth.getHeaders(), params });
  }

  getById(id: number) { return this.http.get<Pedido>(`${this.API}/pedidos/${id}`, { headers: this.auth.getHeaders() }); }
  
  create(data: CreatePedidoRequest) { return this.http.post<Pedido>(`${this.API}/pedidos`, data, { headers: this.auth.getHeaders() }); }
  
  atualizarStatus(id: number, data: AtualizarStatusRequest) { return this.http.patch<Pedido>(`${this.API}/pedidos/${id}/status`, data, { headers: this.auth.getHeaders() }); }
  
  cancelar(id: number) { return this.http.delete(`${this.API}/pedidos/${id}`, { headers: this.auth.getHeaders() }); }
}