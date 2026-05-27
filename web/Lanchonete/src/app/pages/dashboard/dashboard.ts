import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { DashboardService } from '../../services/dashboard-service';
import { Dashboard } from '../../models/dashboard-model';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class DashboardComponent implements OnInit {
  svc = inject(DashboardService);
  cdr = inject(ChangeDetectorRef);
  data: Dashboard | null = null;
  loading = true;

  ngOnInit() {
    setTimeout(() => {
      this.svc.getDashboard().subscribe({
        next: d => { this.data = d; this.loading = false; this.cdr.detectChanges(); },
        error: () => { this.loading = false; this.cdr.detectChanges(); }
      });
    }, 0);
  }

  getBarWidth(valor: number): number {
    const max = Math.max(...(this.data?.faturamentoSemana.map(d => d.valor) ?? [1]));
    return max > 0 ? (valor / max) * 100 : 0;
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