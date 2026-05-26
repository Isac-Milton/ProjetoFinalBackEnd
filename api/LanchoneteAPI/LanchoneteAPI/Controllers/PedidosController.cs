using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LanchoneteAPI.DTOs;
using LanchoneteAPI.Services;

namespace LanchoneteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _service;
    public PedidosController(IPedidoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] DateTime? dataInicio,
        [FromQuery] DateTime? dataFim,
        [FromQuery] string? status)
        => Ok(await _service.GetAllAsync(dataInicio, dataFim, status));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    { var r = await _service.GetByIdAsync(id); return r is null ? NotFound() : Ok(r); }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePedidoRequest req)
    {
        try { return Ok(await _service.CreateAsync(req)); }
        catch (Exception ex) { return BadRequest(new { mensagem = ex.Message }); }
    }

    [HttpPatch("{id}/status")]
    [Authorize(Roles = "Admin,Funcionario")]
    public async Task<IActionResult> AtualizarStatus(int id, [FromBody] AtualizarStatusRequest req)
    { var r = await _service.AtualizarStatusAsync(id, req); return r is null ? NotFound() : Ok(r); }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Funcionario")]
    public async Task<IActionResult> Cancelar(int id)
        => await _service.CancelarAsync(id) ? NoContent() : NotFound();
}