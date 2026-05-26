using Microsoft.EntityFrameworkCore;
using LanchoneteAPI.Data;
using LanchoneteAPI.Models;

namespace LanchoneteAPI.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _db;
    public PedidoRepository(AppDbContext db) => _db = db;

    private IQueryable<Pedido> BaseQuery() =>
        _db.Pedidos
           .Include(p => p.Usuario)
           .Include(p => p.Cupom)
           .Include(p => p.Itens).ThenInclude(i => i.Produto);

    public Task<List<Pedido>> GetAllAsync(DateTime? ini, DateTime? fim, string? status)
    {
        var q = BaseQuery().AsQueryable();
        if (ini.HasValue) q = q.Where(p => p.CriadoEm >= ini.Value);
        if (fim.HasValue) q = q.Where(p => p.CriadoEm <= fim.Value);
        if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<StatusPedido>(status, out var s))
            q = q.Where(p => p.Status == s);
        return q.OrderByDescending(p => p.CriadoEm).ToListAsync();
    }

    public Task<Pedido?> GetByIdAsync(int id) =>
        BaseQuery().FirstOrDefaultAsync(p => p.Id == id);

    public Task<List<Pedido>> GetHojeAsync()
    {
        var hoje = DateTime.UtcNow.Date;
        return BaseQuery().Where(p => p.CriadoEm >= hoje).OrderByDescending(p => p.CriadoEm).ToListAsync();
    }

    public async Task<Pedido> CreateAsync(Pedido p)
    { _db.Pedidos.Add(p); await _db.SaveChangesAsync(); return p; }

    public async Task<Pedido> UpdateAsync(Pedido p)
    { _db.Pedidos.Update(p); await _db.SaveChangesAsync(); return p; }
}