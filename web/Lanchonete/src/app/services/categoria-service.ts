import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth-service';
import { Categoria, CreateCategoriaRequest, UpdateCategoriaRequest } from '../models/categoria-model';

@Injectable({ providedIn: 'root' })

export class CategoriaService {

  private readonly API = 'http://localhost:5007/api';

  constructor(private http: HttpClient, private auth: AuthService) {}

  getAll() { return this.http.get<Categoria[]>(`${this.API}/categorias`, { headers: this.auth.getHeaders() }); }
  
  getById(id: number) { return this.http.get<Categoria>(`${this.API}/categorias/${id}`, { headers: this.auth.getHeaders() }); }
  
  create(data: CreateCategoriaRequest) { return this.http.post<Categoria>(`${this.API}/categorias`, data, { headers: this.auth.getHeaders() }); }
  
  update(id: number, data: UpdateCategoriaRequest) { return this.http.put<Categoria>(`${this.API}/categorias/${id}`, data, { headers: this.auth.getHeaders() }); }
  
  delete(id: number) { return this.http.delete(`${this.API}/categorias/${id}`, { headers: this.auth.getHeaders() }); }
}