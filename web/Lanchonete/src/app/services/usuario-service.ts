import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth-service';
import { Usuario, CreateUsuarioRequest, UpdateUsuarioRequest } from '../models/usuario-model';

@Injectable({ providedIn: 'root' })

export class UsuarioService {

  private readonly API = 'http://localhost:5007/api';

  constructor(private http: HttpClient, private auth: AuthService) {}

  getAll() { return this.http.get<Usuario[]>(`${this.API}/usuarios`, { headers: this.auth.getHeaders() }); }
  
  getById(id: number) { return this.http.get<Usuario>(`${this.API}/usuarios/${id}`, { headers: this.auth.getHeaders() }); }
  
  create(data: CreateUsuarioRequest) { return this.http.post<Usuario>(`${this.API}/usuarios`, data, { headers: this.auth.getHeaders() }); }
  
  update(id: number, data: UpdateUsuarioRequest) { return this.http.put<Usuario>(`${this.API}/usuarios/${id}`, data, { headers: this.auth.getHeaders() }); }
}