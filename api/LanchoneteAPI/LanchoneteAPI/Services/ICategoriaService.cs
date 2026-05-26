using LanchoneteAPI.DTOs;

namespace LanchoneteAPI.Services;

public interface ICategoriaService
{
    Task<List<CategoriaDto>> GetAllAsync();
    Task<CategoriaDto?> GetByIdAsync(int id);
    Task<CategoriaDto> CreateAsync(CreateCategoriaRequest request);
    Task<CategoriaDto?> UpdateAsync(int id, UpdateCategoriaRequest request);
    Task<bool> DeleteAsync(int id);
}