import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cupom, CreateCupomRequest, ValidarCupomRequest, ValidarCupomResponse } from '../models/cupom-model';

@Injectable({ providedIn: 'root' })

export class CupomService {

  private readonly API = 'https://projetofinalbackend-production.up.railway.app/api';

  constructor(private http: HttpClient) {}

  getAll() { return this.http.get<Cupom[]>(`${this.API}/cupons`); }

  create(data: CreateCupomRequest) { return this.http.post<Cupom>(`${this.API}/cupons`, data); }

  delete(id: number) { return this.http.delete(`${this.API}/cupons/${id}`); }

  validar(data: ValidarCupomRequest) { return this.http.post<ValidarCupomResponse>(`${this.API}/cupons/validar`, data); }
}
