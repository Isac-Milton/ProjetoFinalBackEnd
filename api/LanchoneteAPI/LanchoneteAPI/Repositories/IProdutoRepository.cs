using LanchoneteAPI.Models;

namespace LanchoneteAPI.Repositories;

public interface IProdutoRepository
{
    Task<List<Produto>> GetAllAsync();
    Task<Produto?> GetByIdAsync(int id);
    Task<List<Produto>> GetEstoqueBaixoAsync();
    Task<Produto> CreateAsync(Produto p);
    Task<Produto> UpdateAsync(Produto p);
    Task DeleteAsync(int id);
}