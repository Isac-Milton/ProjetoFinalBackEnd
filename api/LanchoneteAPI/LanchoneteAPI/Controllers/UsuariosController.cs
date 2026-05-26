using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LanchoneteAPI.DTOs;
using LanchoneteAPI.Services;

namespace LanchoneteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _service;
    public UsuariosController(IUsuarioService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    { var r = await _service.GetByIdAsync(id); return r is null ? NotFound() : Ok(r); }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUsuarioRequest req)
        => Ok(await _service.CreateAsync(req));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUsuarioRequest req)
    { var r = await _service.UpdateAsync(id, req); return r is null ? NotFound() : Ok(r); }
}