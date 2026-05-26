using LanchoneteAPI.DTOs;

namespace LanchoneteAPI.Services;

public interface IRelatorioService
{
    Task<RelatorioVendasDto> GetRelatorioVendasAsync(RelatorioVendasRequest request);
}