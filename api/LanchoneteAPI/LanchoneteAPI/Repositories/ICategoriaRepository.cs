using LanchoneteAPI.Models;

namespace LanchoneteAPI.Repositories;

public interface ICategoriaRepository
{
    Task<List<Categoria>> GetAllAsync();
    Task<Categoria?> GetByIdAsync(int id);
    Task<Categoria> CreateAsync(Categoria c);
    Task<Categoria> UpdateAsync(Categoria c);
    Task DeleteAsync(int id);
}