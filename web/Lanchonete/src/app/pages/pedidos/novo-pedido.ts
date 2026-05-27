import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { PedidoService } from '../../services/pedido-service';
import { ProdutoService } from '../../services/produto-service';
import { CupomService } from '../../services/cupom-service';
import { Produto } from '../../models/produto-model';
import { CreatePedidoRequest } from '../../models/pedido-model';

@Component({
  selector: 'app-novo-pedido',
  imports: [CommonModule, FormsModule],
  templateUrl: './novo-pedido.html',
  styleUrl: './novo-pedido.css'
})

export class NovoPedido implements OnInit {
  pedidoSvc = inject(PedidoService);
  produtoSvc = inject(ProdutoService);
  cupomSvc = inject(CupomService);
  cdr = inject(ChangeDetectorRef);
  router = inject(Router);

  produtos: Produto[] = [];
  itens: { produtoId: number; produtoNome: string; preco: number; quantidade: number; }[] = [];
  tipo = 'Local';
  formaPagamento = 'Dinheiro';
  nomeCliente = '';
  telefoneCliente = '';
  enderecoEntrega = '';
  observacao = '';
  cupomCodigo = '';
  desconto = 0;
  cupomMensagem = '';
  cupomValido = false;
  erro = '';
  salvando = false;
  busca = '';

  get subtotal() { return this.itens.reduce((s, i) => s + i.preco * i.quantidade, 0); }
  
  get taxaEntrega() { return this.tipo === 'Delivery' ? 8 : 0; }
  
  get total() { return this.subtotal - this.desconto + this.taxaEntrega; }

  get produtosFiltrados() {
    return this.produtos.filter(p => p.nome.toLowerCase().includes(this.busca.toLowerCase()));
  }

  ngOnInit() {
    setTimeout(() => {
      this.produtoSvc.getAll().subscribe(p => {
        this.produtos = p.filter(x => x.disponivel && x.ativo);
        this.cdr.detectChanges();
      });
    }, 0);
  }

  adicionarItem(produto: Produto) {
    const existente = this.itens.find(i => i.produtoId === produto.id);
    if (existente) { existente.quantidade++; return; }
    this.itens.push({ produtoId: produto.id, produtoNome: produto.nome, preco: produto.preco, quantidade: 1 });
  }

  aumentarItem(index: number) {
    this.itens[index].quantidade++;
  }

  diminuirItem(index: number) {
    if (this.itens[index].quantidade > 1) {
      this.itens[index].quantidade--;
    } else {
      this.removerItem(index);
    }
  }

  removerItem(index: number) {
    this.itens.splice(index, 1);
  }

  validarCupom() {
    if (!this.cupomCodigo) {
      this.desconto = 0;
      this.cupomMensagem = '';
      this.cupomValido = false;
      return;
    }
    this.cupomSvc.validar({ codigo: this.cupomCodigo, valorPedido: this.subtotal }).subscribe({
      next: r => {
        this.desconto = r.desconto;
        this.cupomMensagem = r.mensagem ?? '';
        this.cupomValido = r.valido;
        this.cdr.detectChanges();
      },
      error: () => { this.cupomMensagem = 'Cupom inválido.'; this.desconto = 0; this.cupomValido = false; this.cdr.detectChanges(); }
    });
  }

  finalizar() {
    if (this.itens.length === 0) { this.erro = 'Adicione pelo menos um item.'; return; }
    this.salvando = true;
    const pedido: CreatePedidoRequest = {
      tipo: this.tipo,
      formaPagamento: this.formaPagamento,
      cupomCodigo: this.cupomCodigo || undefined,
      nomeCliente: this.nomeCliente || undefined,
      telefoneCliente: this.telefoneCliente || undefined,
      enderecoEntrega: this.enderecoEntrega || undefined,
      observacao: this.observacao || undefined,
      itens: this.itens.map(i => ({ produtoId: i.produtoId, quantidade: i.quantidade }))
    };
    this.pedidoSvc.create(pedido).subscribe({
      next: () => this.router.navigate(['/pedidos']),
      error: (e) => { this.erro = e.error?.mensagem ?? 'Erro ao criar pedido.'; this.salvando = false; }
    });
  }
}