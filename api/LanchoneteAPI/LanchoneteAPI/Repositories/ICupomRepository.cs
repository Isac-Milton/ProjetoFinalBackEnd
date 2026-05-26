using LanchoneteAPI.Models;

namespace LanchoneteAPI.Repositories;

public interface ICupomRepository
{
    Task<List<Cupom>> GetAllAsync();
    Task<Cupom?> GetByIdAsync(int id);
    Task<Cupom?> GetByCodigoAsync(string codigo);
    Task<Cupom> CreateAsync(Cupom c);
    Task<Cupom> UpdateAsync(Cupom c);
    Task DeleteAsync(int id);
}