using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LanchoneteAPI.DTOs;
using LanchoneteAPI.Services;

namespace LanchoneteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _service;
    public RelatoriosController(IRelatorioService service) => _service = service;

    [HttpPost("vendas")]
    public async Task<IActionResult> Vendas([FromBody] RelatorioVendasRequest req)
        => Ok(await _service.GetRelatorioVendasAsync(req));
}