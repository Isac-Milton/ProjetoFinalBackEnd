import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth-service';
import { RelatorioVendas, RelatorioVendasRequest } from '../models/relatorio-model';

@Injectable({ providedIn: 'root' })

export class RelatorioService {
  
  private readonly API = 'http://localhost:5007/api';
  
  constructor(private http: HttpClient, private auth: AuthService) {}

  getRelatorioVendas(data: RelatorioVendasRequest) {
    return this.http.post<RelatorioVendas>(`${this.API}/relatorios/vendas`, data, { headers: this.auth.getHeaders() });
  }
}