using LanchoneteAPI.DTOs;

namespace LanchoneteAPI.Services;

public interface IUsuarioService
{
    Task<List<UsuarioDto>> GetAllAsync();
    Task<UsuarioDto?> GetByIdAsync(int id);
    Task<UsuarioDto> CreateAsync(CreateUsuarioRequest request);
    Task<UsuarioDto?> UpdateAsync(int id, UpdateUsuarioRequest request);
}