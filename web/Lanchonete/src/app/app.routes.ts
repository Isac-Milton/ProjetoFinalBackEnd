import { Routes } from '@angular/router';
import { authGuard } from './guards/auth-guard';
import { adminGuard } from './guards/admin-guard';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login').then(m => m.Login)
  },
  {
    path: '',
    loadComponent: () => import('./components/layout/layout').then(m => m.Layout),
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      {
        path: 'dashboard',
        loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.DashboardComponent)
      },
      {
        path: 'produtos',
        loadComponent: () => import('./pages/produtos/produtos').then(m => m.Produtos)
      },
      {
        path: 'categorias',
        loadComponent: () => import('./pages/categorias/categorias').then(m => m.Categorias)
      },
      {
        path: 'pedidos',
        loadComponent: () => import('./pages/pedidos/pedidos').then(m => m.Pedidos)
      },
      {
        path: 'pedidos/novo',
        loadComponent: () => import('./pages/pedidos/novo-pedido').then(m => m.NovoPedido)
      },
      {
        path: 'estoque',
        loadComponent: () => import('./pages/estoque/estoque').then(m => m.Estoque)
      },
      {
        path: 'cupons',
        loadComponent: () => import('./pages/cupons/cupons').then(m => m.Cupons),
        canActivate: [adminGuard]
      },
      {
        path: 'usuarios',
        loadComponent: () => import('./pages/usuarios/usuarios').then(m => m.Usuarios),
        canActivate: [adminGuard]
      },
      {
        path: 'relatorios',
        loadComponent: () => import('./pages/relatorios/relatorios').then(m => m.Relatorios),
        canActivate: [adminGuard]
      }
    ]
  },
  { path: '**', redirectTo: '' }
];