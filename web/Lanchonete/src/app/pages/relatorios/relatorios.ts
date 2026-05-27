import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RelatorioService } from '../../services/relatorio-service';
import { RelatorioVendas } from '../../models/relatorio-model';

@Component({
  selector: 'app-relatorios',
  imports: [CommonModule, FormsModule],
  templateUrl: './relatorios.html',
  styleUrl: './relatorios.css'
})
export class Relatorios {
  svc = inject(RelatorioService);
  cdr = inject(ChangeDetectorRef);
  relatorio: RelatorioVendas | null = null;
  dataInicio = '';
  dataFim = '';
  loading = false;

  buscar() {
    if (!this.dataInicio || !this.dataFim) return;
    this.loading = true;
    setTimeout(() => {
      this.svc.getRelatorioVendas({ dataInicio: this.dataInicio, dataFim: this.dataFim }).subscribe({
        next: r => { this.relatorio = r; this.loading = false; this.cdr.detectChanges(); },
        error: () => { this.loading = false; this.cdr.detectChanges(); }
      });
    }, 0);
  }

  getKeys(obj: Record<string, number>) { return Object.keys(obj); }
}