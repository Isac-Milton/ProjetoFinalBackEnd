import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  auth = inject(AuthService);
  router = inject(Router);
  email = '';
  senha = '';
  erro = '';
  loading = false;

  onSubmit() {
    this.loading = true;
    this.erro = '';
    this.auth.login({ email: this.email, senha: this.senha }).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: () => { this.erro = 'Email ou senha inválidos.'; this.loading = false; }
    });
  }
}