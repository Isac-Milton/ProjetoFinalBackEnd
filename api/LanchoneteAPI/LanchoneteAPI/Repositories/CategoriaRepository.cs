using Microsoft.EntityFrameworkCore;
using LanchoneteAPI.Data;
using LanchoneteAPI.Models;

namespace LanchoneteAPI.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _db;
    public CategoriaRepository(AppDbContext db) => _db = db;

    public Task<List<Categoria>> GetAllAsync() =>
        _db.Categorias.Include(c => c.Produtos).OrderBy(c => c.Nome).ToListAsync();

    public Task<Categoria?> GetByIdAsync(int id) =>
        _db.Categorias.Include(c => c.Produtos).FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Categoria> CreateAsync(Categoria c)
    { _db.Categorias.Add(c); await _db.SaveChangesAsync(); return c; }

    public async Task<Categoria> UpdateAsync(Categoria c)
    { _db.Categorias.Update(c); await _db.SaveChangesAsync(); return c; }

    public async Task DeleteAsync(int id)
    {
        var c = await _db.Categorias.FindAsync(id);
        if (c != null) { _db.Categorias.Remove(c); await _db.SaveChangesAsync(); }
    }
}