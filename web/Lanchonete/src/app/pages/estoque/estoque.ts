import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProdutoService } from '../../services/produto-service';
import { Produto } from '../../models/produto-model';

@Component({
  selector: 'app-estoque',
  imports: [CommonModule, FormsModule],
  templateUrl: './estoque.html',
  styleUrl: './estoque.css'
})

export class Estoque implements OnInit {
  
  svc = inject(ProdutoService);
  cdr = inject(ChangeDetectorRef);
  produtos: Produto[] = [];
  modalAberto = false;
  produtoSelecionado: Produto | null = null;
  quantidade = 0;
  operacao = 'adicionar';
  salvando = false;
  busca = '';

  get produtosFiltrados() {
    return this.produtos.filter(p => p.nome.toLowerCase().includes(this.busca.toLowerCase()));
  }

  ngOnInit() { setTimeout(() => this.carregar(), 0); }

  carregar() {
    this.svc.getAll().subscribe(p => { this.produtos = p; this.cdr.detectChanges(); });
  }

  abrirModal(p: Produto) {
    this.produtoSelecionado = p;
    this.quantidade = 0;
    this.operacao = 'adicionar';
    this.modalAberto = true;
  }

  fecharModal() { this.modalAberto = false; }

  salvar() {
    if (!this.produtoSelecionado) return;
    this.salvando = true;
    this.svc.atualizarEstoque(this.produtoSelecionado.id, { quantidade: this.quantidade, operacao: this.operacao }).subscribe({
      next: () => { this.carregar(); this.fecharModal(); this.salvando = false; },
      error: () => this.salvando = false
    });
  }
}