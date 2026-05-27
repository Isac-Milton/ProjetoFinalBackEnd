import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CupomService } from '../../services/cupom-service';
import { Cupom, CreateCupomRequest } from '../../models/cupom-model';

@Component({
  selector: 'app-cupons',
  imports: [CommonModule, FormsModule],
  templateUrl: './cupons.html',
  styleUrl: './cupons.css'
})

export class Cupons implements OnInit {
  
  svc = inject(CupomService);
  cdr = inject(ChangeDetectorRef);
  cupons: Cupom[] = [];
  modalAberto = false;
  modalConfirmacao = false;
  cupomParaExcluir: Cupom | null = null;
  salvando = false;
  erro = '';
  form: any = this.formVazio();

  formVazio() {
    return { codigo: '', descricao: '', tipoDesconto: 'Percentual', valorDesconto: 0, valorMinimoPedido: null, limiteUsos: null, validoAte: '' };
  }

  ngOnInit() { setTimeout(() => this.carregar(), 0); }

  carregar() {
    this.svc.getAll().subscribe(c => { this.cupons = c; this.cdr.detectChanges(); });
  }

  abrirModal() { this.form = this.formVazio(); this.erro = ''; this.modalAberto = true; }
  
  fecharModal() { this.modalAberto = false; this.erro = ''; }

  salvar() {
    this.salvando = true;
    this.svc.create(this.form as CreateCupomRequest).subscribe({
      next: () => { this.carregar(); this.fecharModal(); this.salvando = false; this.cdr.detectChanges(); },
      error: () => { this.erro = 'Erro ao salvar.'; this.salvando = false; this.cdr.detectChanges(); }
    });
  }

  confirmarExclusao(c: Cupom) { this.cupomParaExcluir = c; this.modalConfirmacao = true; }
  
  cancelarExclusao() { this.cupomParaExcluir = null; this.modalConfirmacao = false; }

  excluir() {
    if (!this.cupomParaExcluir) return;
    this.svc.delete(this.cupomParaExcluir.id).subscribe({
      next: () => { this.modalConfirmacao = false; this.cupomParaExcluir = null; this.carregar(); },
      error: () => { this.modalConfirmacao = false; }
    });
  }
}