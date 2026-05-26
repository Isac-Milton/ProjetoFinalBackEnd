using Microsoft.EntityFrameworkCore;
using LanchoneteAPI.Data;
using LanchoneteAPI.Models;

namespace LanchoneteAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _db;
    public UsuarioRepository(AppDbContext db) => _db = db;

    public Task<List<Usuario>> GetAllAsync() =>
        _db.Usuarios.OrderBy(u => u.Nome).ToListAsync();

    public Task<Usuario?> GetByIdAsync(int id) =>
        _db.Usuarios.FindAsync(id).AsTask();

    public Task<Usuario?> GetByEmailAsync(string email) =>
        _db.Usuarios.FirstOrDefaultAsync(u => u.Email == email.ToLower());

    public async Task<Usuario> CreateAsync(Usuario u)
    { u.Email = u.Email.ToLower(); _db.Usuarios.Add(u); await _db.SaveChangesAsync(); return u; }

    public async Task<Usuario> UpdateAsync(Usuario u)
    { _db.Usuarios.Update(u); await _db.SaveChangesAsync(); return u; }
}