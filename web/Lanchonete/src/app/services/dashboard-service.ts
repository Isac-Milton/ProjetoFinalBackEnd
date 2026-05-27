import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth-service';
import { Dashboard } from '../models/dashboard-model';

@Injectable({ providedIn: 'root' })

export class DashboardService {

  private readonly API = 'https://projetofinalbackend-production.up.railway.app/api';

  constructor(private http: HttpClient, private auth: AuthService) {}
  
  getDashboard() {
    return this.http.get<Dashboard>(`${this.API}/dashboard`, { headers: this.auth.getHeaders() });
  }
}
