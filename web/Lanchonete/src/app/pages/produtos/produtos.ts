import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProdutoService } from '../../services/produto-service';
import { CategoriaService } from '../../services/categoria-service';
import { AuthService } from '../../services/auth-service';
import { Produto, CreateProdutoRequest, UpdateProdutoRequest } from '../../models/produto-model';
import { Categoria } from '../../models/categoria-model';

@Component({
  selector: 'app-produtos',
  imports: [CommonModule, FormsModule],
  templateUrl: './produtos.html',
  styleUrl: './produtos.css'
})
export class Produtos implements OnInit {
  produtoSvc = inject(ProdutoService);
  categoriaSvc = inject(CategoriaService);
  auth = inject(AuthService);
  cdr = inject(ChangeDetectorRef);
  produtos: Produto[] = [];
  categorias: Categoria[] = [];
  modalAberto = false;
  modalConfirmacao = false;
  editando: Produto | null = null;
  produtoParaExcluir: Produto | null = null;
  salvando = false;
  erro = '';
  busca = '';
  form: any = this.formVazio();

  formVazio() {
    return { nome: '', descricao: '', preco: 0, categoriaId: 0, estoqueQuantidade: 0, estoqueMinimo: 5, disponivel: true, ativo: true };
  }

  ngOnInit() {
    setTimeout(() => {
      this.carregar();
      this.categoriaSvc.getAll().subscribe(c => { this.categorias = c; this.cdr.detectChanges(); });
    }, 0);
  }

  carregar() {
    this.produtoSvc.getAll().subscribe(p => { this.produtos = p; this.cdr.detectChanges(); });
  }

  get produtosFiltrados() {
    return this.produtos.filter(p => p.nome.toLowerCase().includes(this.busca.toLowerCase()));
  }

  abrirModal() { this.editando = null; this.form = this.formVazio(); this.modalAberto = true; }
  fecharModal() { this.modalAberto = false; this.erro = ''; }

  editar(p: Produto) {
    this.editando = p;
    this.form = { nome: p.nome, descricao: p.descricao, preco: p.preco, categoriaId: p.categoriaId, estoqueQuantidade: p.estoqueQuantidade, estoqueMinimo: p.estoqueMinimo, disponivel: p.disponivel, ativo: p.ativo };
    this.modalAberto = true;
  }

  salvar() {
    this.salvando = true;
    const obs = this.editando
      ? this.produtoSvc.update(this.editando.id, this.form as UpdateProdutoRequest)
      : this.produtoSvc.create(this.form as CreateProdutoRequest);
    obs.subscribe({
      next: () => { this.carregar(); this.fecharModal(); this.salvando = false; },
      error: () => { this.erro = 'Erro ao salvar.'; this.salvando = false; }
    });
  }

  confirmarExclusao(p: Produto) {
    this.produtoParaExcluir = p;
    this.modalConfirmacao = true;
  }

  cancelarExclusao() {
    this.produtoParaExcluir = null;
    this.modalConfirmacao = false;
  }

  excluir() {
    if (!this.produtoParaExcluir) return;
    this.produtoSvc.delete(this.produtoParaExcluir.id).subscribe(() => {
      this.modalConfirmacao = false;
      this.produtoParaExcluir = null;
      this.carregar();
    });
  }
}