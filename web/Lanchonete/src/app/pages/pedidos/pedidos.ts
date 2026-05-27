import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { PedidoService } from '../../services/pedido-service';
import { Pedido } from '../../models/pedido-model';

@Component({
  selector: 'app-pedidos',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './pedidos.html',
  styleUrl: './pedidos.css'
})

export class Pedidos implements OnInit {
  
  svc = inject(PedidoService);
  cdr = inject(ChangeDetectorRef);
  pedidos: Pedido[] = [];
  statusFiltro = '';
  dataInicio = '';
  dataFim = '';
  statusList = ['Pendente','Confirmado','EmPreparo','ProntoParaEntrega','EmEntrega','Entregue','Cancelado'];

  modalCancelar: Pedido | null = null;

  ngOnInit() { setTimeout(() => this.carregar(), 0); }

  carregar() {
    this.svc.getAll(this.dataInicio || undefined, this.dataFim || undefined, this.statusFiltro || undefined)
      .subscribe(p => { this.pedidos = p; this.cdr.detectChanges(); });
  }

  proximosStatus(atual: string): string[] {
    const fluxo: Record<string, string[]> = {
      'Pendente': ['Confirmado'],
      'Confirmado': ['EmPreparo'],
      'EmPreparo': ['ProntoParaEntrega'],
      'ProntoParaEntrega': ['EmEntrega', 'Entregue'],
      'EmEntrega': ['Entregue']
    };
    return fluxo[atual] ?? [];
  }

  mudarStatus(p: Pedido, status: string) {
    this.svc.atualizarStatus(p.id, { status }).subscribe(() => this.carregar());
  }

  abrirModalCancelar(p: Pedido) {
    this.modalCancelar = p;
  }

  confirmarCancelar() {
    if (!this.modalCancelar) return;
    this.svc.cancelar(this.modalCancelar.id).subscribe(() => {
      this.modalCancelar = null;
      this.carregar();
    });
  }

  fecharModalCancelar() {
    this.modalCancelar = null;
  }

  getStatusClass(status: string): string {
    const map: Record<string, string> = {
      'Pendente': 'bg-warning text-dark',
      'Confirmado': 'bg-primary text-white',
      'EmPreparo': 'bg-warning text-dark',
      'ProntoParaEntrega': 'bg-info text-white',
      'EmEntrega': 'bg-info text-dark',
      'Entregue': 'bg-success text-white',
      'Cancelado': 'bg-danger text-white'
    };
    return map[status] ?? 'bg-secondary text-white';
  }
}