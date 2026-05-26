using Microsoft.EntityFrameworkCore;
using LanchoneteAPI.Data;
using LanchoneteAPI.Models;

namespace LanchoneteAPI.Repositories;

public class CupomRepository : ICupomRepository
{
    private readonly AppDbContext _db;
    public CupomRepository(AppDbContext db) => _db = db;

    public Task<List<Cupom>> GetAllAsync() =>
        _db.Cupons.OrderByDescending(c => c.CriadoEm).ToListAsync();

    public async Task<Cupom?> GetByIdAsync(int id) =>
        await _db.Cupons.FindAsync(id);

    public Task<Cupom?> GetByCodigoAsync(string codigo) =>
        _db.Cupons.FirstOrDefaultAsync(c => c.Codigo == codigo.ToUpper());

    public async Task<Cupom> CreateAsync(Cupom c)
    { c.Codigo = c.Codigo.ToUpper(); _db.Cupons.Add(c); await _db.SaveChangesAsync(); return c; }

    public async Task<Cupom> UpdateAsync(Cupom c)
    { _db.Cupons.Update(c); await _db.SaveChangesAsync(); return c; }

    public async Task DeleteAsync(int id)
    {
        var c = await _db.Cupons.FindAsync(id);
        if (c != null) { c.Ativo = false; await _db.SaveChangesAsync(); }
    }
}