using LanchoneteAPI.DTOs;

namespace LanchoneteAPI.Services;

public interface ICupomService
{
    Task<List<CupomDto>> GetAllAsync();
    Task<CupomDto> CreateAsync(CreateCupomRequest request);
    Task<bool> DeleteAsync(int id);
    Task<ValidarCupomResponse> ValidarAsync(ValidarCupomRequest request);
}