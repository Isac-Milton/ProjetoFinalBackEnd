using LanchoneteAPI.DTOs;

namespace LanchoneteAPI.Services;

public interface IPedidoService
{
    Task<List<PedidoDto>> GetAllAsync(DateTime? ini, DateTime? fim, string? status);
    Task<PedidoDto?> GetByIdAsync(int id);
    Task<PedidoDto> CreateAsync(CreatePedidoRequest request);
    Task<PedidoDto?> AtualizarStatusAsync(int id, AtualizarStatusRequest request);
    Task<bool> CancelarAsync(int id);
}