using LanchoneteAPI.Models;

namespace LanchoneteAPI.Repositories;

public interface IUsuarioRepository
{
    Task<List<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario> CreateAsync(Usuario u);
    Task<Usuario> UpdateAsync(Usuario u);
}