using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LanchoneteAPI.DTOs;
using LanchoneteAPI.Services;

namespace LanchoneteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CuponsController : ControllerBase
{
    private readonly ICupomService _service;
    public CuponsController(ICupomService service) => _service = service;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCupomRequest req)
        => Ok(await _service.CreateAsync(req));

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
        => await _service.DeleteAsync(id) ? NoContent() : NotFound();

    [HttpPost("validar")]
    public async Task<IActionResult> Validar([FromBody] ValidarCupomRequest req)
        => Ok(await _service.ValidarAsync(req));
}