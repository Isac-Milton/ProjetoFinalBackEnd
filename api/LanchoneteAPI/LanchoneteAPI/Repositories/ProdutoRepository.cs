using Microsoft.EntityFrameworkCore;
using LanchoneteAPI.Data;
using LanchoneteAPI.Models;

namespace LanchoneteAPI.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _db;
    public ProdutoRepository(AppDbContext db) => _db = db;

    public Task<List<Produto>> GetAllAsync() =>
        _db.Produtos.Include(p => p.Categoria).OrderBy(p => p.Nome).ToListAsync();

    public Task<Produto?> GetByIdAsync(int id) =>
        _db.Produtos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);

    public Task<List<Produto>> GetEstoqueBaixoAsync() =>
        _db.Produtos.Include(p => p.Categoria)
           .Where(p => p.Ativo && p.EstoqueQuantidade <= p.EstoqueMinimo)
           .ToListAsync();

    public async Task<Produto> CreateAsync(Produto p)
    { _db.Produtos.Add(p); await _db.SaveChangesAsync(); return p; }

    public async Task<Produto> UpdateAsync(Produto p)
    { p.AtualizadoEm = DateTime.UtcNow; _db.Produtos.Update(p); await _db.SaveChangesAsync(); return p; }

    public async Task DeleteAsync(int id)
    {
        var p = await _db.Produtos.FindAsync(id);
        if (p != null) { p.Ativo = false; await _db.SaveChangesAsync(); }
    }
}