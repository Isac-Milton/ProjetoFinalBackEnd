using LanchoneteAPI.DTOs;

namespace LanchoneteAPI.Services;

public interface IProdutoService
{
    Task<List<ProdutoDto>> GetAllAsync();
    Task<ProdutoDto?> GetByIdAsync(int id);
    Task<List<ProdutoDto>> GetEstoqueBaixoAsync();
    Task<ProdutoDto> CreateAsync(CreateProdutoRequest request);
    Task<ProdutoDto?> UpdateAsync(int id, UpdateProdutoRequest request);
    Task<ProdutoDto?> AtualizarEstoqueAsync(int id, AtualizarEstoqueRequest request);
    Task<bool> DeleteAsync(int id);
}