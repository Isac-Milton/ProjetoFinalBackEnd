using LanchoneteAPI.Models;

namespace LanchoneteAPI.Repositories;

public interface IPedidoRepository
{
    Task<List<Pedido>> GetAllAsync(DateTime? ini, DateTime? fim, string? status);
    Task<Pedido?> GetByIdAsync(int id);
    Task<List<Pedido>> GetHojeAsync();
    Task<Pedido> CreateAsync(Pedido p);
    Task<Pedido> UpdateAsync(Pedido p);
}