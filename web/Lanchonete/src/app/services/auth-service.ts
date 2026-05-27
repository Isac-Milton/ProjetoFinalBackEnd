import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginRequest, LoginResponse } from '../models/auth-model';

@Injectable({ providedIn: 'root' })

export class AuthService {

  private readonly API = 'https://projetofinalbackend-production.up.railway.app/api';
  private _usuario = new BehaviorSubject<LoginResponse | null>(this.loadUser());

  usuario$ = this._usuario.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  get usuario() { return this._usuario.value; }
  
  get isLoggedIn() { return !!this._usuario.value; }
  
  get isAdmin() { return this._usuario.value?.perfil === 'Admin'; }
  
  get isFuncionario() { return ['Admin', 'Funcionario'].includes(this._usuario.value?.perfil ?? ''); }

  getHeaders(): HttpHeaders {
    const raw = localStorage.getItem('lanchonete_user');
    const token = raw ? JSON.parse(raw).token : null;
    return new HttpHeaders({ 'Authorization': `Bearer ${token}` });
  }

  login(data: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.API}/auth/login`, data).pipe(
      tap(res => {
        localStorage.setItem('lanchonete_user', JSON.stringify(res));
        this._usuario.next(res);
      })
    );
  }

  logout() {
    localStorage.removeItem('lanchonete_user');
    this._usuario.next(null);
    this.router.navigate(['/login']);
  }

  private loadUser(): LoginResponse | null {
    const raw = localStorage.getItem('lanchonete_user');
    return raw ? JSON.parse(raw) : null;
  }
}
