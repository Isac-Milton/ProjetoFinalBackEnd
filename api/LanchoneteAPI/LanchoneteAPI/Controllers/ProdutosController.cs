using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LanchoneteAPI.DTOs;
using LanchoneteAPI.Services;

namespace LanchoneteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _service;
    public ProdutosController(IProdutoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    { var r = await _service.GetByIdAsync(id); return r is null ? NotFound() : Ok(r); }

    [HttpGet("estoque-baixo")]
    public async Task<IActionResult> GetEstoqueBaixo()
        => Ok(await _service.GetEstoqueBaixoAsync());

    [HttpPost]
    [Authorize(Roles = "Admin,Funcionario")]
    public async Task<IActionResult> Create([FromBody] CreateProdutoRequest req)
        => Ok(await _service.CreateAsync(req));

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Funcionario")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProdutoRequest req)
    { var r = await _service.UpdateAsync(id, req); return r is null ? NotFound() : Ok(r); }

    [HttpPatch("{id}/estoque")]
    [Authorize(Roles = "Admin,Funcionario")]
    public async Task<IActionResult> AtualizarEstoque(int id, [FromBody] AtualizarEstoqueRequest req)
    { var r = await _service.AtualizarEstoqueAsync(id, req); return r is null ? NotFound() : Ok(r); }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
        => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}