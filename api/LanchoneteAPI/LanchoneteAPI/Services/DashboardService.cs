using LanchoneteAPI.DTOs;
using LanchoneteAPI.Models;
using LanchoneteAPI.Repositories;

namespace LanchoneteAPI.Services;

public class DashboardService : IDashboardService
{
    private readonly IPedidoRepository _pedidoRepo;
    private readonly IProdutoRepository _produtoRepo;

    public DashboardService(IPedidoRepository pedidoRepo, IProdutoRepository produtoRepo)
    { _pedidoRepo = pedidoRepo; _produtoRepo = produtoRepo; }

    public async Task<DashboardDto> GetDashboardAsync()
    {
        var pedidosHoje = await _pedidoRepo.GetHojeAsync();
        var ativos = pedidosHoje.Where(p => p.Status != StatusPedido.Cancelado).ToList();
        var emAberto = ativos.Where(p => p.Status != StatusPedido.Entregue).ToList();
        var produtosBaixo = await _produtoRepo.GetEstoqueBaixoAsync();

        var ultimos7 = await _pedidoRepo.GetAllAsync(DateTime.UtcNow.AddDays(-7), null, null);
        var maisVendidos = ultimos7
            .Where(p => p.Status != StatusPedido.Cancelado)
            .SelectMany(p => p.Itens)
            .GroupBy(i => i.Produto?.Nome ?? "")
            .Select(g => new ProdutoMaisVendidoDto(g.Key, g.Sum(i => i.Quantidade), g.Sum(i => i.Subtotal)))
            .OrderByDescending(p => p.Quantidade).Take(5).ToList();

        var faturSemana = Enumerable.Range(0, 7)
            .Select(i => DateTime.UtcNow.AddDays(-i).Date)
            .Select(dia =>
            {
                var doDia = ultimos7.Where(p => p.CriadoEm.Date == dia && p.Status != StatusPedido.Cancelado).ToList();
                return new FaturamentoDiarioDto(dia.ToString("dd/MM"), doDia.Sum(p => p.Total), doDia.Count);
            }).Reverse().ToList();

        var recentes = pedidosHoje.Take(10)
            .Select(p => new PedidoRecenteDto(p.Id, p.NomeCliente ?? p.Usuario?.Nome, p.Total, p.Status.ToString(), p.CriadoEm))
            .ToList();

        return new DashboardDto(ativos.Count, ativos.Sum(p => p.Total), emAberto.Count, produtosBaixo.Count, recentes, maisVendidos, faturSemana);
    }
}