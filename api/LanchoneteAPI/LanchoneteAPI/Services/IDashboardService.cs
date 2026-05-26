using LanchoneteAPI.DTOs;

namespace LanchoneteAPI.Services;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync();
}