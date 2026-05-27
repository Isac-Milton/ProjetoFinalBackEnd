import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CategoriaService } from '../../services/categoria-service';
import { Categoria, CreateCategoriaRequest, UpdateCategoriaRequest } from '../../models/categoria-model';

@Component({
  selector: 'app-categorias',
  imports: [CommonModule, FormsModule],
  templateUrl: './categorias.html',
  styleUrl: './categorias.css'
})
export class Categorias implements OnInit {
  svc = inject(CategoriaService);
  cdr = inject(ChangeDetectorRef);
  categorias: Categoria[] = [];
  modalAberto = false;
  modalConfirmacao = false;
  editando: Categoria | null = null;
  categoriaParaExcluir: Categoria | null = null;
  salvando = false;
  loading = true;
  erro = '';
  erroGeral = '';
  form: any = { nome: '', descricao: '' };

  ngOnInit() { setTimeout(() => this.carregar(), 0); }

  carregar() {
    this.loading = true;
    this.erroGeral = '';
    this.svc.getAll().subscribe({
      next: c => { this.categorias = c; this.loading = false; this.cdr.detectChanges(); },
      error: () => { this.erroGeral = 'Erro ao carregar categorias.'; this.loading = false; this.cdr.detectChanges(); }
    });
  }

  abrirModal() { this.editando = null; this.form = { nome: '', descricao: '' }; this.modalAberto = true; }
  fecharModal() { this.modalAberto = false; this.erro = ''; }

  editar(c: Categoria) {
    this.editando = c;
    this.form = { nome: c.nome, descricao: c.descricao, ativo: c.ativo };
    this.modalAberto = true;
  }

  salvar() {
    this.salvando = true;
    const obs = this.editando
      ? this.svc.update(this.editando.id, this.form as UpdateCategoriaRequest)
      : this.svc.create(this.form as CreateCategoriaRequest);
    obs.subscribe({
      next: () => { this.carregar(); this.fecharModal(); this.salvando = false; this.cdr.detectChanges(); },
      error: () => { this.erro = 'Erro ao salvar.'; this.salvando = false; this.cdr.detectChanges(); }
    });
  }

  confirmarExclusao(c: Categoria) { this.categoriaParaExcluir = c; this.modalConfirmacao = true; }
  cancelarExclusao() { this.categoriaParaExcluir = null; this.modalConfirmacao = false; }

  excluir() {
    if (!this.categoriaParaExcluir) return;
    this.svc.delete(this.categoriaParaExcluir.id).subscribe({
      next: () => { this.modalConfirmacao = false; this.categoriaParaExcluir = null; this.carregar(); },
      error: () => { this.erro = 'Erro ao excluir.'; this.modalConfirmacao = false; this.cdr.detectChanges(); }
    });
  }
}