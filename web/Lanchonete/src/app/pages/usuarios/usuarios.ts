import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UsuarioService } from '../../services/usuario-service';
import { Usuario, CreateUsuarioRequest, UpdateUsuarioRequest } from '../../models/usuario-model';

@Component({
  selector: 'app-usuarios',
  imports: [CommonModule, FormsModule],
  templateUrl: './usuarios.html',
  styleUrl: './usuarios.css'
})
export class Usuarios implements OnInit {
  svc = inject(UsuarioService);
  cdr = inject(ChangeDetectorRef);
  usuarios: Usuario[] = [];
  modalAberto = false;
  editando: Usuario | null = null;
  salvando = false;
  erro = '';
  form: any = this.formVazio();

  formVazio() {
    return { nome: '', email: '', senha: '', perfil: 'Funcionario', telefone: '', endereco: '' };
  }

  ngOnInit() { setTimeout(() => this.carregar(), 0); }

  carregar() {
    this.svc.getAll().subscribe(u => { this.usuarios = u; this.cdr.detectChanges(); });
  }

  abrirModal() { this.editando = null; this.form = this.formVazio(); this.modalAberto = true; }
  fecharModal() { this.modalAberto = false; this.erro = ''; }

  editar(u: Usuario) {
    this.editando = u;
    this.form = { nome: u.nome, email: u.email, senha: '', perfil: u.perfil, telefone: u.telefone, endereco: u.endereco };
    this.modalAberto = true;
  }

  salvar() {
    this.salvando = true;
    const obs = this.editando
      ? this.svc.update(this.editando.id, { nome: this.form.nome, telefone: this.form.telefone, endereco: this.form.endereco, ativo: true } as UpdateUsuarioRequest)
      : this.svc.create(this.form as CreateUsuarioRequest);
    obs.subscribe({
      next: () => { this.carregar(); this.fecharModal(); this.salvando = false; this.cdr.detectChanges(); },
      error: (e) => { this.erro = e.error?.mensagem ?? 'Erro ao salvar.'; this.salvando = false; this.cdr.detectChanges(); }
    });
  }
}