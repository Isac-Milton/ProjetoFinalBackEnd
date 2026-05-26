using LanchoneteAPI.DTOs;
using LanchoneteAPI.Models;
using LanchoneteAPI.Repositories;

namespace LanchoneteAPI.Services;

public class RelatorioService : IRelatorioService
{
    private readonly IPedidoRepository _repo;
    public RelatorioService(IPedidoRepository repo) => _repo = repo;

    public async Task<RelatorioVendasDto> GetRelatorioVendasAsync(RelatorioVendasRequest r)
    {
        var todos = await _repo.GetAllAsync(r.DataInicio, r.DataFim, null);
        var pedidos = todos.Where(p => p.Status != StatusPedido.Cancelado).ToList();

        var porDia = pedidos.GroupBy(p => p.CriadoEm.Date).OrderBy(g => g.Key)
            .Select(g => new FaturamentoDiarioDto(g.Key.ToString("dd/MM"), g.Sum(p => p.Total), g.Count())).ToList();

        var maisVendidos = pedidos.SelectMany(p => p.Itens)
            .GroupBy(i => i.Produto?.Nome ?? "")
            .Select(g => new ProdutoMaisVendidoDto(g.Key, g.Sum(i => i.Quantidade), g.Sum(i => i.Subtotal)))
            .OrderByDescending(p => p.Quantidade).Take(10).ToList();

        var porStatus = todos.GroupBy(p => p.Status.ToString()).ToDictionary(g => g.Key, g => g.Count());

        var porCategoria = pedidos.SelectMany(p => p.Itens)
            .GroupBy(i => i.Produto?.Categoria?.Nome ?? "Outros")
            .ToDictionary(g => g.Key, g => g.Sum(i => i.Subtotal));

        var total = pedidos.Sum(p => p.Total);
        return new RelatorioVendasDto(total, pedidos.Count,
            pedidos.Count > 0 ? total / pedidos.Count : 0,
            porDia, maisVendidos, porStatus, porCategoria);
    }
}